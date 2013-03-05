var ChildrenCtrl = function ($scope, $location, childService, messageService) {
    $scope.getChildren = function () {
        childService.query({ format: 'json' }, function (children) {
            $scope.children = [];
            $scope.children = $scope.children.concat(children);
        });
    };

    $scope.addChild = function () {
        childService.save($scope.todo, function (task) {
            $scope.children.push(task);
            $scope.todo.Task = '';
        });
    };

    $scope.delete = function () {
        var deleteTodos = $.grep($scope.children, function (todo) {
            return todo.Completed;
        });
        var deleteIds = [];
        angular.forEach(deleteTodos, function (value, key) {
            deleteIds.push(value.Id);
        });
        childService.deleteAll({
            format: 'json',
            Ids: deleteIds
        }, function () {
            var keepTodos = $.grep($scope.children, function (todo) {
                return !todo.Completed;
            });
            $scope.children = [];
            $scope.children = keepTodos;
        });
    };
    $scope.getChildren();
};
//Default service injection doesn't work with minification, so a manual injection is necessary. The one liner injection
//TodoApp.controller('TodoListCtrl', ['$scope', '$location', 'todoService', 'loadingService', 'messageService', function ($scope, $location, todoService, loadingService, messageService) { ... }]);
//doesn't seem to work for me as I'd prefer this 
ChildrenCtrl.$inject = ['$scope', '$location', 'childService', 'messageService'];