using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using BusinessLogic.Properties;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class MenuService : IMenuService
    {
        private IChildService ChildService { get; set; }
        private IChildFormService ChildFormService { get; set; }
        private ICourtService CourtService { get; set; }
        private IOutputService OutputService { get; set; }
        private IDeviationsService DeviationsService { get; set; }
        private IPreexistingSupportFormService PreexistingSupportFormService { get; set; }
        private IOtherChildrenService OtherChildrenService { get; set; }
        private IExtraExpenseFormService ExtraExpenseFormService { get; set; }
        private IIncomeService IncomeService { get; set; }
        private const string ParentingText = "Parenting Plan";
        private const string SuccessIcon = "icon-ok icon-green";
        private const string FinancialText = "Financial Form";
        private const string DomesticText = "Mediation Agreement";
        public MenuService(IChildService childService, IChildFormService childFormService, ICourtService courtService, IOutputService outputService, IDeviationsService deviationsService, IPreexistingSupportFormService preexistingSupportFormService, IOtherChildrenService otherChildrenService, IIncomeService incomeService,
            IExtraExpenseFormService extraExpenseFormService)
        {
            ChildService = childService;
            ChildFormService = childFormService;
            CourtService = courtService;
            OutputService = outputService;
            DeviationsService = deviationsService;
            PreexistingSupportFormService = preexistingSupportFormService;
            OtherChildrenService = otherChildrenService;
            IncomeService = incomeService;
            ExtraExpenseFormService = extraExpenseFormService;
        }

        public List<MenuItem> Get(string route, long userId, bool showAdminMenu, bool showAttorneyMenu, bool isAuthenticated = false)
        {
            //Always has Home Link
            var menuList = new List<MenuItem>
                {
                    new MenuItem
                        {
                            itemClass = "active",
                            path = "/",
                            iconClass = "icon icon-home",
                            text = "Home",
                        }
                };
            if (!isAuthenticated || userId == 0)
            {
                menuList.Add(new MenuItem()
                    {
                        itemClass = "",
                        path = "/#/Account/Login",
                        iconClass = "icon icon-signin",
                        text = "Login"
                    });
                var registerMenu = GetRegisterMenu();
                menuList.Add(registerMenu);
                return menuList;
            }
            if (showAdminMenu)
            {
                var adminMenu = GetAdminMenu(userId);
                menuList.Add(adminMenu);
            } 
            else if (showAttorneyMenu)
            {
                var attorneyMenu = GetAttorneyMenu(userId);
                menuList.Add(attorneyMenu);                
            }
            else if (UserIsAtStarterStage(userId))
            {
                var starterMenu = GetStarterMenu(userId);
                //Get Completed Status of forms for the menu
                menuList.Add(starterMenu);
            }
            else
            {
                menuList.Add(GetMediationMenu(userId));
                var children = ChildService.GetByUserId(userId);
                if (children.Count > 0)
                {
                    menuList.Add(GetParentingPlanMenu(userId, children[0]));
                    menuList.Add(GetFinancialFormMenu(userId, children[0]));
                }
                menuList.Add(GetOutputMenu(userId, menuList));
            }

            menuList.Add(new MenuItem
                {
                    itemClass = "",
                    path = "/#/Account/Logoff",
                    iconClass = "icon icon-share-alt",
                    text = "Log out",
                });
            return menuList;
        }

        private MenuItem GetRegisterMenu()
        {
            var menuList = new List<FormMenuItem>
                {
                    new FormMenuItem
                        {
                            formName = "",
                            text = "Register User",
                            iconClass = "",
                            path = "/Account/Register/",
                            pathIdentifier = "Parenting",
                            itemClass = "",
                            hasLink = true,
                        },
                    new FormMenuItem
                        {
                            formName = "",
                            text = "Register Law Firm",
                            iconClass = "",
                            path = "/Administrator/Pricing/",
                            pathIdentifier = "Property",
                            itemClass = "",
                            hasLink = true,
                        },
                };
            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                pathIdentifier = "Register",
                iconClass = "icon icon-th-list",
                text = "Register",
                showSubMenu = false,
                subMenuItems = menuList
            }; 
        }

        private MenuItem GetAttorneyMenu(long userId)
        {
            return new MenuItem
            {
                itemClass = "",
                path = "/#/Attorney/AttorneyPage/Attorney/" + userId,
                pathIdentifier = "Attorney",
                iconClass = "icon icon-user",
                text = "Attorney Profile",
                showSubMenu = false,
            };
        }

        private MenuItem GetOutputMenu(long userId, List<MenuItem> mainList)
        {
            var parentDisabled = IsOutputDisabled(ParentingText, mainList);
            var domesticDisabled = IsOutputDisabled(DomesticText, mainList);
            var financialDisabled = IsOutputDisabled(FinancialText, mainList);
            var scheduleEDisabled = !HasScheduleE(userId); 
            var scheduleBDisabled = !HasScheduleB(userId);
            var menuList = new List<FormMenuItem>
                {
                    new FormMenuItem
                        {
                            formName = "Parenting",
                            text = "Parenting",
                            iconClass = "",
                            path = "/Output/Parenting/User/" + userId,
                            pathIdentifier = "Parenting",
                            itemClass = "",
                            disabled = parentDisabled                            
                        },
                    new FormMenuItem
                        {
                            formName = "Domestic",
                            text = "Mediation Agreement",
                            iconClass = "",
                            path = "/Output/DomesticMediation/User/" + userId,
                            pathIdentifier = "Property",
                            itemClass = "",
                            disabled = domesticDisabled
                        },
                    new FormMenuItem
                        {
                            formName = "ScheduleA",
                            text = "Schedule A",
                            iconClass = "",
                            path = "/Output/ScheduleA/User/" + userId,
                            pathIdentifier = "ScheduleA",
                            itemClass = "",
                            disabled = financialDisabled
                        },
                    new FormMenuItem
                        {
                            formName = "ScheduleB",
                            text = "Schedule B",
                            iconClass = "",
                            path = "/Output/ScheduleB/User/" + userId,
                            pathIdentifier = "ScheduleB",
                            itemClass = "",
                            disabled = scheduleBDisabled || financialDisabled
                        },
                    new FormMenuItem
                        {
                            formName = "ScheduleD",
                            text = "Schedule D",
                            iconClass = "",
                            path = "/Output/ScheduleD/User/" + userId,
                            pathIdentifier = "ScheduleD",
                            itemClass = "",
                            disabled = financialDisabled
                        },
                    new FormMenuItem
                        {
                            formName = "ScheduleE",
                            text = "Schedule E",
                            iconClass = "",
                            path = "/Output/ScheduleE/User/" + userId,
                            pathIdentifier = "ScheduleE",
                            itemClass = "",
                            disabled = scheduleEDisabled || financialDisabled
                        },
                    new FormMenuItem
                        {
                            formName = "ChildSupport",
                            text = "Child Support Wkst.",
                            iconClass = "",
                            path = "/Output/ChildSupport/User/" + userId,
                            pathIdentifier = "ChildSupport",
                            itemClass = "",
                            disabled = financialDisabled
                        },
                    new FormMenuItem
                        {
                            formName = "CSA",
                            text = "Child Support Add.",
                            iconClass = "",
                            path = "/Output/CSA/User/" + userId,
                            pathIdentifier = "CSA",
                            itemClass = "",
                            disabled = financialDisabled
                        },
                };
            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                pathIdentifier = "Output",
                iconClass = "icon icon-th-list",
                text = "Output",
                showSubMenu = false,
                subMenuItems = menuList
            };
        }

        public bool HasScheduleE(long userId)
        {
            var deviations = DeviationsService.GetByUserId(userId) as Deviations;
            var extraExpenses = ExtraExpenseFormService.GetByUserId(userId) as ExtraExpenseForm;            
            return (deviations != null && deviations.Deviation == (int)YesNo.Yes && extraExpenses != null && extraExpenses.HasExtraExpenses == (int)YesNo.Yes);
        }

        public bool HasScheduleB(long userId)
        {
            var preexistingForm = PreexistingSupportFormService.GetByUserId(userId);
            if (preexistingForm != null && preexistingForm.Support == (int)YesNo.Yes)
                return true;
            var otherPreexistingForm = PreexistingSupportFormService.GetByUserId(userId, isOtherParent: true);
            if (otherPreexistingForm != null && otherPreexistingForm.Support == (int)YesNo.Yes)
                return true;
            if (IncomeService.HasNonW2Income(userId))
                return true;
            return OtherChildrenService.HasOtherChildren(userId);
        }

        /// <summary>
        /// Check to see if output should be disabled
        /// </summary>
        /// <param name="menuText"></param>
        /// <param name="mainList"></param>
        /// <returns></returns>
        private bool IsOutputDisabled(string menuText, IEnumerable<MenuItem> mainList)
        {
            var menuItem = mainList.FirstOrDefault(x => x.text == menuText);
            if (menuItem != null && menuItem.subMenuItems.All(x => x.iconClass == SuccessIcon))
                return false;
            return true;
        }

        #region Menu Logic Helpers
        private bool UserIsAtStarterStage(long userId)
        {
            var incompleteForms = OutputService.GetStarterIncompleteForms(userId);
            return incompleteForms.Count != 0;
        }
        
        private static List<FormMenuItem> AdjustIconClass(IEnumerable<IncompleteForm> incompleteForms, List<FormMenuItem> menuList)
        {
            //if menuitem is not in incomplete forms list, add the green check mark to it. 
            foreach (var formMenuItem in menuList.Where(formMenuItem => incompleteForms.All(x => x.Path.ToUpper() != formMenuItem.path.ToUpper())))
            {
                formMenuItem.iconClass = SuccessIcon;
            }
            return menuList;
        }

        #endregion

        #region GetMenus
        private MenuItem GetStarterMenu(long userId)
        {
            var menuList = new List<FormMenuItem>
                {
                    new FormMenuItem
                    {
                        formName = "BetaAgreement",
                        text = "Beta Agreement",
                        iconClass = "",
                        path = "/Starter/BetaAgreement/User/" + userId,     
                        pathIdentifier = "BetaAgreement",
                        itemClass = ""
                    },
                    new FormMenuItem
                    {
                        formName = "Introduction",
                        text = "Introduction",
                        iconClass = "",
                        path = "/Starter/Introduction/User/" + userId,     
                        pathIdentifier = "Introduction",
                        itemClass = ""
                    },
                    new FormMenuItem
                    {
                        formName = "Court",
                        text = "Court",
                        iconClass = "",
                        path = "/Starter/Court/User/" + userId,     
                        pathIdentifier = "Court",
                        itemClass = ""
                    },
                    new FormMenuItem
                        {
                            formName = "Participant",
                            text = "Participants",
                            iconClass = "",
                            path = "/Starter/Participant/User/" + userId,
                            pathIdentifier = "Participant",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Children",
                            text = "Children",
                            iconClass = "",
                            path = "/Starter/Children/User/" + userId,
                            pathIdentifier = "Children",
                            itemClass = ""
                        }
                };
            menuList = AdjustIconClass(OutputService.GetStarterIncompleteForms(userId), menuList);
            //Introduction doesn't have a form, let's make the item class to never be complete initially.
            menuList.First(x => x.formName == "Introduction").iconClass = "";
            return new MenuItem
                {
                    itemClass = "submenu",
                    path = "",
                    pathIdentifier = Resources.StarterFormName,
                    iconClass = "icon icon-th-list",
                    text = "Preliminary Information",
                    showSubMenu = false,
                    subMenuItems = menuList
                };
        }
        private MenuItem GetMediationMenu(long userId)
        {
            var menuList = new List<FormMenuItem>
                {
                    new FormMenuItem
                        {
                            formName = "House",
                            text = "Marital House",
                            iconClass = "",
                            path = "/Domestic/House/User/" + userId,
                            pathIdentifier = "House",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Property",
                            text = "Personal Property",
                            iconClass = "",
                            path = "/Domestic/Property/User/" + userId,
                            pathIdentifier = "Property",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Vehicle",
                            text = "Vehicles",
                            iconClass = "",
                            path = "/Domestic/Vehicle/User/" + userId,
                            pathIdentifier = "Vehicle",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Debt",
                            text = "Debt",
                            iconClass = "",
                            path = "/Domestic/Debt/User/" + userId,
                            pathIdentifier = "Debt",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Asset",
                            text = "Assets",
                            iconClass = "",
                            path = "/Domestic/Asset/User/" + userId,
                            pathIdentifier = "Asset",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "HealthInsurance",
                            text = "Health Insurance",
                            iconClass = "",
                            path = "/Domestic/HealthInsurance/User/" + userId,
                            pathIdentifier = "HealthInsurance",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Spousal",
                            text = "Spousal Support",
                            iconClass = "",
                            path = "/Domestic/Spousal/User/" + userId,
                            pathIdentifier = "Spousal",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Tax",
                            text = "Taxes",
                            iconClass = "",
                            path = "/Domestic/Tax/User/" + userId,
                            pathIdentifier = "Tax",
                            itemClass = ""
                        },
                };
            menuList = AdjustIconClass(OutputService.GetDomesticIncompleteForms(userId), menuList);

            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                pathIdentifier = "Domestic",
                iconClass = "icon icon-th-list",
                text = DomesticText,
                showSubMenu = false,
                subMenuItems = menuList
            };
        }
        private MenuItem GetParentingPlanMenu(long userId, Child firstChild)
        {
            var menuList = new List<FormMenuItem>
                {
                    new FormMenuItem
                        {
                            formName = "Privacy",
                            text = "Supervision",
                            iconClass = "",
                            path = "/Parenting/Supervision/User/" + userId,
                            pathIdentifier = "Supervision",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Information",
                            text = "Information",
                            iconClass = "",
                            path = "/Parenting/Information/User/" + userId,
                            pathIdentifier = "Information",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Decisions",
                            text = "Decisions",
                            iconClass = "",
                            path = "/Parenting/Decision/User/" + userId + "/Child/" + firstChild.Id,
                            pathIdentifier = "Decision",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Responsibility",
                            text = "Responsibility",
                            iconClass = "",
                            path = "/Parenting/Responsibility/User/" + userId,
                            pathIdentifier = "Responsibility",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Communication",
                            text = "Communication",
                            iconClass = "",
                            path = "/Parenting/Communication/User/" + userId,
                            pathIdentifier = "Communication",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Schedule",
                            text = "Schedule",
                            iconClass = "",
                            path = "/Parenting/Schedule/User/" + userId,
                            pathIdentifier = "Schedule",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Holidays",
                            text = "Holidays",
                            iconClass = "",
                            path = "/Parenting/Holiday/User/" + userId + "/Child/" + firstChild.Id,
                            pathIdentifier = "Holiday",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Addendum",
                            text = "Special Considerations",
                            iconClass = "",
                            path = "/Parenting/Addendum/User/" + userId,
                            pathIdentifier = "Addendum",
                            itemClass = ""
                        },
                };
            menuList = AdjustIconClass(OutputService.GetParentingIncompleteForms(userId), menuList);
            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                pathIdentifier = "Parenting",
                iconClass = "icon icon-th-list",
                text = ParentingText,
                showSubMenu = false,
                subMenuItems = menuList
            };


        }
        private MenuItem GetFinancialFormMenu(long userId, Child firstChild)
        {
            var menuList = new List<FormMenuItem>
                {
                    new FormMenuItem
                        {
                            formName = "ChildCare",
                            text = "Child Care",
                            iconClass = "",
                            path = "/Financial/ChildCare/User/" + userId + "/Child/" + firstChild.Id,
                            pathIdentifier = "ChildCare",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "ChildSupport",
                            text = "Child Support",
                            iconClass = "",
                            path = "/Financial/ChildSupport/User/" + userId,
                            pathIdentifier = "ChildSupport",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "ExtraExpenses",
                            text = "Extra Expenses",
                            iconClass = "",
                            path = "/Financial/ExtraExpense/User/" + userId + "/Child/" + firstChild.Id,
                            pathIdentifier = "ExtraExpense",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Health",
                            text = "Health Insurance",
                            iconClass = "",
                            path = "/Financial/Health/User/" + userId,
                            pathIdentifier = "Health",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Income",
                            text = "Income (Father)",
                            iconClass = "",
                            path = "/Financial/Income/User/" + userId + "/false",
                            pathIdentifier = "Income",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "SocialSecurity",
                            text = "Social Security (Father)",
                            iconClass = "",
                            path = "/Financial/SocialSecurity/User/" + userId + "/false",
                            pathIdentifier = "SocialSecurity",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Support",
                            text = "Preexisting Support (Father)",
                            iconClass = "",
                            path = "/Financial/Support/User/" + userId + "/false",
                            pathIdentifier = "Support",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "OtherChildren",
                            text = "Other Children (Father)",
                            iconClass = "",
                            path = "/Financial/OtherChild/User/" + userId + "/false",
                            pathIdentifier = "OtherChild",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Income",
                            text = "Income (Mother)",
                            iconClass = "",
                            path = "/Financial/Income/User/" + userId + "/true",
                            pathIdentifier = "Income",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "SocialSecurity",
                            text = "Social Security (Mother)",
                            iconClass = "",
                            path = "/Financial/SocialSecurity/User/" + userId+ "/true",
                            pathIdentifier = "SocialSecurity",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Support",
                            text = "Preexisting Support (Mother)",
                            iconClass = "",
                            path = "/Financial/Support/User/" + userId + "/true",
                            pathIdentifier = "Support",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "OtherChildren",
                            text = "Other Children (Mother)",
                            iconClass = "",
                            path = "/Financial/OtherChild/User/" + userId + "/true",
                            pathIdentifier = "OtherChild",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Deviations",
                            text = "Deviations",
                            iconClass = "",
                            path = "/Financial/Deviation/User/" + userId,
                            pathIdentifier = "Deviation",
                            itemClass = ""
                        },
                };
            menuList = AdjustIconClass(OutputService.GetFinancialIncompleteForms(userId), menuList);

            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                pathIdentifier = "Financial",
                iconClass = "icon icon-th-list",
                text = FinancialText,
                showSubMenu = false,
                subMenuItems = menuList
            };

        }
        private MenuItem GetAdminMenu(long userId)
        {
            return new MenuItem
            {
                itemClass = "",
                path = "/#/Administrator/ClientCases/Admin/" + userId,
                pathIdentifier = "Administrator",
                iconClass = "icon icon-user",
                text = "Administration",
                showSubMenu = false,
            };

        }
        #endregion
    }
}
