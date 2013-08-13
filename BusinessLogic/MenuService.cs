using System.Collections.Generic;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using Models;

namespace BusinessLogic
{
    public class MenuService : IMenuService
    {
        private IChildService ChildService { get; set; }
        private IChildFormService ChildFormService { get; set; }
        private ICourtService CourtService { get; set; }
        public MenuService(IChildService childService, IChildFormService childFormService, ICourtService courtService)
        {
            ChildService = childService;
            ChildFormService = childFormService;
            CourtService = courtService;
        }

        public List<MenuItem> Get(string route, long userId, bool isAuthenticated = false)
        {
            //Always has Home Link
            var menuList = new List<MenuItem>
                {
                    new MenuItem
                        {
                            itemClass = "active",
                            path = "#/",
                            iconClass = "icon icon-home",
                            text = "Home",
                        }
                };
            if (!isAuthenticated)
            {
                return menuList;
            }
            if (UserIsAtStarterStage(userId))
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
            }

            menuList.Add(new MenuItem
                {
                    itemClass = "",
                    path = "/Account/LogOff",
                    iconClass = "icon icon-share-alt",
                    text = "Log out",
                });
            return menuList;
        }

        #region Menu Logic Helpers
        private bool UserIsAtStarterStage(long userId)
        {
            var childForm = ChildFormService.GetByUserId(userId);
            if (childForm.UserId == userId)
                return false;
            return true;
        }
        #endregion

        private enum MenuType
        {
            Starter,
            DomesticMediation,
            Parenting,
            Financial
        };
        #region GetMenus
        private MenuItem GetStarterMenu(long userId)
        {
            var menuList = new List<FormMenuItem>
                {
                    new FormMenuItem
                    {
                        formName = "Court",
                        text = "Court",
                        iconClass = "",
                        path = "/Starter/Court/" + userId,     
                        pathIdentifier = "Court",
                        itemClass = ""
                    },
                    new FormMenuItem
                        {
                            formName = "Participant",
                            text = "Participants",
                            iconClass = "",
                            path = "/Starter/Participant/" + userId,
                            pathIdentifier = "Participant",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Children",
                            text = "Children",
                            iconClass = "",
                            path = "/Starter/Children/" + userId,
                            pathIdentifier = "Children",
                            itemClass = ""
                        }
                };
            return new MenuItem
                {
                    itemClass = "submenu",
                    path = "",
                    pathIdentifier = "Starter",
                    iconClass = "icon icon-th-list",
                    text = "Starter",
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
                            path = "/Domestic/House/" + userId,
                            pathIdentifier = "House",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Property",
                            text = "Personal Property",
                            iconClass = "",
                            path = "/Domestic/Property/" + userId,
                            pathIdentifier = "Property",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Vehicle",
                            text = "Vehicles",
                            iconClass = "",
                            path = "/Domestic/Vehicle/" + userId,
                            pathIdentifier = "Vehicle",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Debt",
                            text = "Debt",
                            iconClass = "",
                            path = "/Domestic/Debt/" + userId,
                            pathIdentifier = "Debt",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Asset",
                            text = "Assets",
                            iconClass = "",
                            path = "/Domestic/Asset/" + userId,
                            pathIdentifier = "Asset",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "HealthInsurance",
                            text = "Health Insurance",
                            iconClass = "",
                            path = "/Domestic/HealthInsurance/" + userId,
                            pathIdentifier = "HealthInsurance",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Spousal",
                            text = "Spousal Support",
                            iconClass = "",
                            path = "/Domestic/Spousal/" + userId,
                            pathIdentifier = "Spousal",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Tax",
                            text = "Taxes",
                            iconClass = "",
                            path = "/Domestic/Tax/" + userId,
                            pathIdentifier = "Tax",
                            itemClass = ""
                        },
                };
            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                pathIdentifier = "Domestic",
                iconClass = "icon icon-th-list",
                text = "Mediation Agreement",
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
                            path = "/Parenting/Supervision/" + userId,
                            pathIdentifier = "Supervision",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Information",
                            text = "Information",
                            iconClass = "",
                            path = "/Parenting/Information/" + userId,
                            pathIdentifier = "Information",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Decisions",
                            text = "Decisions",
                            iconClass = "",
                            path = "/Parenting/Decision/" + userId + "/" + firstChild.Id,
                            pathIdentifier = "Decision",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Responsibility",
                            text = "Responsibility",
                            iconClass = "",
                            path = "/Parenting/Responsibility/" + userId,
                            pathIdentifier = "Responsibility",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Communication",
                            text = "Communication",
                            iconClass = "",
                            path = "/Parenting/Communication/" + userId,
                            pathIdentifier = "Communication",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Schedule",
                            text = "Schedule",
                            iconClass = "",
                            path = "/Parenting/Schedule/" + userId,
                            pathIdentifier = "Schedule",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Holidays",
                            text = "Holidays",
                            iconClass = "",
                            path = "/Parenting/Holiday/" + userId + "/" + firstChild.Id,
                            pathIdentifier = "Holiday",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Addendum",
                            text = "Special Considerations",
                            iconClass = "",
                            path = "/Parenting/Addendum/" + userId,
                            pathIdentifier = "Addendum",
                            itemClass = ""
                        },
                };
            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                pathIdentifier = "Parenting",
                iconClass = "icon icon-th-list",
                text = "Parenting Plan",
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
                            path = "/Financial/ChildCare/" + userId + "/" + firstChild.Id,
                            pathIdentifier = "ChildCare",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "ExtraExpenses",
                            text = "Extra Expenses",
                            iconClass = "",
                            path = "/Financial/ExtraExpense/" + userId + "/" + firstChild.Id,
                            pathIdentifier = "ExtraExpense",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Health",
                            text = "Health Insurance",
                            iconClass = "",
                            path = "/Financial/Health/" + userId,
                            pathIdentifier = "Health",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Income",
                            text = "Income (Father)",
                            iconClass = "",
                            path = "/Financial/Income/" + userId + "/false",
                            pathIdentifier = "Income",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "SocialSecurity",
                            text = "Social Security (Father)",
                            iconClass = "",
                            path = "/Financial/SocialSecurity/" + userId + "/false",
                            pathIdentifier = "SocialSecurity",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Support",
                            text = "Preexisting Support (Father)",
                            iconClass = "",
                            path = "/Financial/Support/" + userId + "/false",
                            pathIdentifier = "Support",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "OtherChildren",
                            text = "Other Children (Father)",
                            iconClass = "",
                            path = "/Financial/OtherChild/" + userId + "/false",
                            pathIdentifier = "OtherChild",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Income",
                            text = "Income (Mother)",
                            iconClass = "",
                            path = "/Financial/Income/" + userId + "/true",
                            pathIdentifier = "Income",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "SocialSecurity",
                            text = "Social Security (Mother)",
                            iconClass = "",
                            path = "/Financial/SocialSecurity/" + userId+ "/true",
                            pathIdentifier = "SocialSecurity",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Support",
                            text = "Preexisting Support (Mother)",
                            iconClass = "",
                            path = "/Financial/Support/" + userId + "/true",
                            pathIdentifier = "Support",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "OtherChildren",
                            text = "Other Children (Mother)",
                            iconClass = "",
                            path = "/Financial/OtherChild/" + userId + "/true",
                            pathIdentifier = "OtherChild",
                            itemClass = ""
                        },
                    new FormMenuItem
                        {
                            formName = "Deviations",
                            text = "Deviations",
                            iconClass = "",
                            path = "/Financial/Deviations/" + userId,
                            pathIdentifier = "Deviations",
                            itemClass = ""
                        },
                };
            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                pathIdentifier = "Financial",
                iconClass = "icon icon-th-list",
                text = "Financial Form",
                showSubMenu = false,
                subMenuItems = menuList
            };

        }
        #endregion

        #region GetCompletedStatus
        private List<MenuItem> GetCompletedFormStatus(MenuType menuType, List<MenuItem> menuItems)
        {
            switch (menuType)
            {
                case MenuType.Starter:
                    return GetStarterStatus(menuItems);
                default:
                    break;
            }
            return menuItems;
        }
        private List<MenuItem> GetStarterStatus(List<MenuItem> menuItems)
        {
            return menuItems;
        }
        #endregion
    }
}
