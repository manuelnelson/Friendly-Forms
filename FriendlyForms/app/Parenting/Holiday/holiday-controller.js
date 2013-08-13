var HolidayCtrl = function ($scope, $routeParams, $location, holidayService, menuService, genericService, $rootScope) {
    //#region Intialize
    $scope.path = $location.path();
    $scope.showErrors = false;
    $scope.showMessage = false;
    $rootScope.currentScope = $scope;
    holidayService.children.get({ UserId: $routeParams.userId }, function (data) {
        $scope.children = data.Children;
        $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
        $scope.childName = $scope.children[$scope.childNdx].Name;
        $scope.menuPath = '/Parenting/Holiday/' + $routeParams.userId + '/' + $scope.children[0].Id;
        if (!menuService.isActive($scope.menuPath)) {
            menuService.setActive($scope.menuPath);
        }
    });
    //#endregion

    //#region Event Handlers
    $scope.getChildHoliday = function (childId) {
        $scope.holiday = holidayService.holidays.get({ ChildId: childId }, function () {
            if (typeof $scope.holiday.Id == 'undefined' || $scope.holiday.Id == 0) {
                //see if garlic has something stored            
                $scope.holiday = $.jStorage.get($scope.path);
            }
        });
        holidayService.extraHolidays.get({ ChildId: childId }, function (data) {
            if (data.ExtraHolidays.length === 0) {
                $scope.extraHolidays = [];
            }
            $scope.extraHolidays = data.ExtraHolidays;
        });
    };
    $scope.addExtraHoliday = function () {
        if ($scope.extraHolidayForm.$invalid) {
            return;
        }
        $scope.extraHoliday.ChildId = $routeParams.childId;
        holidayService.extraHolidays.save(null, $scope.extraHoliday, function (data) {
            $scope.extraHolidays.push(data);
            $scope.extraHoliday.HolidayName = '';
            $scope.extraHoliday.HolidayFather = -1;
            $scope.extraHoliday.HolidayMother = -1;
        });

    };
    $scope.submit = function (noNavigate, callback) {
        if ($scope.holidayForm.$invalid) {
            menuService.setSubMenuIconClass($scope.menuPath, 'icon-pencil icon-red');
            var value = genericService.getFormInput('#holidayForm');
            $.jStorage.set($scope.path, value);
            $scope.showErrors = true;
            if (callback)
                callback();
            return;
        }
        $.jStorage.deleteKey($scope.path);
        $scope.holiday.UserId = $routeParams.userId;
        $scope.holiday.ChildId = $routeParams.childId;
        if (typeof $scope.holiday.Id == 'undefined' || $scope.holiday.Id == 0) {
            holidayService.holidays.save(null, $scope.holiday, function () {
            });
        } else {
            holidayService.holidays.update({ Id: $scope.holiday.Id }, $scope.holiday, function () {
            });
        }
        menuService.setSubMenuIconClass($scope.menuPath, 'icon-ok icon-green');
        if ($scope.extraHolidays.length > 0) {
            //Post each extraHoliday
            var completedCount = 0;
            _.each($scope.extraHolidays, function (item) {
                holidayService.extraHolidays.update(null, item, function () {
                    //Wait till all extraHolidays are updated before doing callback
                    completedCount++;
                    if ($scope.extraHolidays.length === completedCount && callback) {
                        callback();
                    }
                });
            });
        } else {
            if (callback)
                callback();
        }
    };
    $scope.previousChild = function () {
        $scope.submit(false, function () {
            $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
            if ($scope.childNdx < 0) {
                //Navigate else where
                return;
            }
            $scope.childNdx = $scope.childNdx - 1;
            var childId = $scope.children[$scope.childNdx].Id;
            $location.path('/Parenting/Holiday/' + $routeParams.userId + '/' + childId);
        });
    };
    $scope.nextChild = function () {
        $scope.submit(false, function () {
            $scope.childNdx = _.indexOf(_.pluck($scope.children, 'Id'), parseInt($routeParams.childId));
            if ($scope.childNdx === ($scope.children.length - 1)) {
                //Navigate to next item
                return;
            }
            $scope.childNdx = $scope.childNdx + 1;
            var childId = $scope.children[$scope.childNdx].Id;
            menuService.setSubMenuIconClass($scope.path, 'icon-ok icon-green');
            $location.path('/Parenting/Holiday/' + $routeParams.userId + '/' + childId);
        });
    };
    $scope.copyChild = function (childId) {
        //first submit current child
        $scope.submit(false, function () {
            if (childId === 0) {
                //copy to all children
                _.each($scope.children, function (item) {
                    if (item.childId != $routeParams.childId)
                        copyToChild(item.Id);
                });
            } else {
                copyToChild(childId);
            }
        });
    };

    function copyToChild(childId) {
        //add extra holidays if they don't already exist for 
        //this is what we are copying
        var currentExtraHolidays = angular.copy($scope.extraHolidays);
        holidayService.extraHolidays.get({ ChildId: childId }, function (data) {
            _.each(data.ExtraHolidays, function (extraHoliday) {
                var match = _.find(currentExtraHolidays, function (item) {
                    return item.HolidayName == extraHoliday.HolidayName;
                });
                //if we have a match, we just want to overwrite, so update Id and ChildId
                if (match) {
                    match.Id = extraHoliday.Id;
                    match.ChildId = extraHoliday.ChildId;
                    holidayService.extraHolidays.update(null, match, function () {
                    });
                    //remove current holidays
                    currentExtraHolidays = _.reject(currentExtraHolidays, function (item) {
                        return item.HolidayName == extraHoliday.HolidayName;
                    });
                }
            });
            //Add new extra holidays
            _.each(currentExtraHolidays, function (item) {
                item.ChildId = childId;
                holidayService.extraHolidays.save(null, item, function () {
                });
            });
            //Now...update holiday
            holidayService.holidays.get({ ChildId: childId }, function (holiday) {
                var copyHoliday = angular.copy($scope.holiday);
                copyHoliday.UserId = $routeParams.userId;
                copyHoliday.ChildId = childId;
                //update if holiday exists, post otherwise
                if (typeof holiday.Id !== 'undefined') {
                    copyHoliday.Id = holiday.Id;
                    holidayService.holidays.update(null, copyHoliday, function () {
                        $scope.showMessage = true;
                    });
                } else {
                    holidayService.holidays.save(null, copyHoliday, function () {
                        $scope.showMessage = true;
                    });
                }
            });
        });
    }

    $scope.matchValue = function (parentType) {
        switch (parentType) {
            case 'Father':
                setFatherValues($scope.holiday.allFather);
                setMotherValues(otherValue($scope.holiday.allFather));
                break;
            case 'Mother':
                setMotherValues($scope.holiday.allMother);
                setFatherValues(otherValue($scope.holiday.allMother));
                break;
            default:
        }
    };
    $scope.changeHoliday = function (modelName) {
        var value = $scope.holiday[modelName];
        var otherModelName;
        if (modelName.lastIndexOf("Mother") >= 0 && modelName.lastIndexOf("Mother") > modelName.lastIndexOf("Father")) {
            //some names have mother twice, always appearing first (example MothersDayMother). To get the last mother, just ignore the first
            var hackedName = modelName.substr(1);
            otherModelName = hackedName.replace("Mother", "Father");
            otherModelName = modelName.substr(0, 1) + otherModelName;
        } else {
            var hackedName = modelName.substr(1);
            otherModelName = hackedName.replace("Father", "Mother");
            otherModelName = modelName.substr(0, 1) + otherModelName;
        }
        var newValue = otherValue(value);
        $scope.holiday[otherModelName] = newValue;

    };
    function setFatherValues(value) {
        $scope.holiday.ChristmasFather = value;
        $scope.holiday.MlkFather = value;
        $scope.holiday.FallBreakFather = value;
        $scope.holiday.SpringBreakFather = value;
        $scope.holiday.ThanksgivingFather = value;
        $scope.holiday.PresidentsFather = value;
        $scope.holiday.MothersFather = value;
        $scope.holiday.MemorialFather = value;
        $scope.holiday.FathersFather = value;
        $scope.holiday.IndependenceFather = value;
        $scope.holiday.LaborFather = value;
        $scope.holiday.HalloweenFather = value;
        $scope.holiday.ChildrensFather = value;
        $scope.holiday.MothersBdayFather = value;
        $scope.holiday.FathersBdayFather = value;
        $scope.holiday.ReligiousFather = value;
    };
    function setMotherValues(value) {
        $scope.holiday.ChristmasMother = value;
        $scope.holiday.FallBreakMother = value;
        $scope.holiday.SpringBreakMother = value;
        $scope.holiday.ThanksgivingMother = value;
        $scope.holiday.PresidentsMother = value;
        $scope.holiday.MothersMother = value;
        $scope.holiday.MemorialMother = value;
        $scope.holiday.FathersMother = value;
        $scope.holiday.IndependenceMother = value;
        $scope.holiday.LaborMother = value;
        $scope.holiday.MlkMother = value;
        $scope.holiday.HalloweenMother = value;
        $scope.holiday.MothersBdayMother = value;
        $scope.holiday.ChildrensMother = value;
        $scope.holiday.FathersBdayMother = value;
        $scope.holiday.ReligiousMother = value;
    };
    function otherValue(value) {
        switch (parseInt(value)) {
            case 1:
                return 2;
            case 2:
                return 1;
            case 3:
                return 4;
            case 4:
                return 3;
        }
        return 0;
    };
    //#endregion

    $scope.getChildHoliday($routeParams.childId);

};
HolidayCtrl.$inject = ['$scope', '$routeParams', '$location', 'holidayService', 'menuService', 'genericService', '$rootScope'];