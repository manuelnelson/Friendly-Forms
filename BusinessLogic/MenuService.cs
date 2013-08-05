using System.Collections.Generic;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using Models;

namespace BusinessLogic
{
    public class MenuService : IMenuService
    {
        public List<MenuItem> GetMenuList(string route, User user, bool isAuthenticated = false)
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
            if (UserIsAtStarterStage(user))
            {
                menuList.Add(GetStarterMenu(user.Id));
            }
            else
            {
                menuList.Add(GetMediationMenu(user.Id));
                if (UserHasChildren(user))
                {
                    menuList.Add(GetParentingPlanMenu(user.Id));
                    menuList.Add(GetFinancialFormMenu(user.Id));
                }
            }

            menuList.Add(new MenuItem()
                {
                    itemClass = "",
                    path = "/Account/LogOff",
                    iconClass = "icon icon-share-alt",
                    text = "Log out",
                });
            return menuList;
        }

        #region Menu Logic Helpers
        private bool UserHasChildren(User user)
        {
            return true;
        }

        private bool UserIsAtStarterStage(User user)
        {
            return true;
        }
        #endregion

        #region GetMenus
        private MenuItem GetStarterMenu(long userId)
        {
            var menuList = new List<MenuItem>
                {
                    new MenuItem()
                        {
                            formName = "Court",
                            text = "Court",
                            iconClass = "",
                            path = "/Starter/Court/" + userId,
                            itemClass = ""
                        },
                    new MenuItem()
                        {
                            formName = "Participant",
                            text = "Participants",
                            iconClass = "",
                            path = "/Starter/Participant/" + userId,
                            itemClass = ""
                        },
                    new MenuItem()
                        {
                            formName = "Children",
                            text = "Children",
                            iconClass = "",
                            path = "/Starter/Children/" + userId,
                            itemClass = ""
                        }
                };
            return new MenuItem
                {
                    itemClass = "submenu",
                    path = "",
                    iconClass = "icon icon-th-list",
                    text = "Starter",
                    showSubMenu = false,
                    subMenuItems = menuList
                };
        }
        private MenuItem GetMediationMenu(long userId)
        {
            var menuList = new List<MenuItem>
                {
                    new MenuItem
                        {
                            formName = "House",
                            text = "Marital House",
                            iconClass = "",
                            path = "/Domestic/House/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Property",
                            text = "Personal Property",
                            iconClass = "",
                            path = "/Domestic/Property/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Vehicle",
                            text = "Vehicles",
                            iconClass = "",
                            path = "/Domestic/Vehicle/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Children",
                            text = "Children",
                            iconClass = "",
                            path = "/Domestic/Children/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Debt",
                            text = "Debt",
                            iconClass = "",
                            path = "/Domestic/Debt/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Asset",
                            text = "Assets",
                            iconClass = "",
                            path = "/Domestic/Asset/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "HealthInsurance",
                            text = "Health Insurance",
                            iconClass = "",
                            path = "/Domestic/HealthInsurance/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Spousal",
                            text = "Spousal Support",
                            iconClass = "",
                            path = "/Domestic/Spousal/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Tax",
                            text = "Taxes",
                            iconClass = "",
                            path = "/Domestic/Tax/" + userId,
                            itemClass = ""
                        },
                };
            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                iconClass = "icon icon-th-list",
                text = "Mediation Agreement",
                showSubMenu = false,
                subMenuItems = menuList
            };
        }
        private MenuItem GetParentingPlanMenu(long userId)
        {
            var menuList = new List<MenuItem>
                {
                    new MenuItem
                        {
                            formName = "Supervision",
                            text = "Supervision",
                            iconClass = "",
                            path = "/Parenting/Supervision/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Information",
                            text = "Information",
                            iconClass = "",
                            path = "/Parenting/Information/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Decisions",
                            text = "Decisions",
                            iconClass = "",
                            path = "/Parenting/Vehicle/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Responsibility",
                            text = "Responsibility",
                            iconClass = "",
                            path = "/Parenting/Responsibility/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Communication",
                            text = "Communication",
                            iconClass = "",
                            path = "/Parenting/Communication/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Schedule",
                            text = "Schedule",
                            iconClass = "",
                            path = "/Parenting/Schedule/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Holidays",
                            text = "Holidays",
                            iconClass = "",
                            path = "/Parenting/Holidays/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Addendum",
                            text = "Special Considerations",
                            iconClass = "",
                            path = "/Parenting/Addendum/" + userId,
                            itemClass = ""
                        },
                };
            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                iconClass = "icon icon-th-list",
                text = "Parenting Plan",
                showSubMenu = false,
                subMenuItems = menuList
            };


        }
        private MenuItem GetFinancialFormMenu(long userId)
        {
            var menuList = new List<MenuItem>
                {
                    new MenuItem
                        {
                            formName = "ChildCare",
                            text = "Child Care",
                            iconClass = "",
                            path = "/Financial/ChildCare/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "ExtraExpenses",
                            text = "Extra Expenses",
                            iconClass = "",
                            path = "/Financial/ExtraExpenses/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Health",
                            text = "Health Insurance",
                            iconClass = "",
                            path = "/Financial/Health/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Income",
                            text = "Income (Father)",
                            iconClass = "",
                            path = "/Financial/Income/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "SocialSecurity",
                            text = "Social Security (Father)",
                            iconClass = "",
                            path = "/Financial/SocialSecurity/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Support",
                            text = "Preexisting Support (Father)",
                            iconClass = "",
                            path = "/Financial/Support/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "OtherChildren",
                            text = "Other Children (Father)",
                            iconClass = "",
                            path = "/Financial/OtherChildren/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Income",
                            text = "Income (Mother)",
                            iconClass = "",
                            path = "/Financial/Income/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "SocialSecurity",
                            text = "Social Security (Mother)",
                            iconClass = "",
                            path = "/Financial/SocialSecurity/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Support",
                            text = "Preexisting Support (Mother)",
                            iconClass = "",
                            path = "/Financial/Support/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "OtherChildren",
                            text = "Other Children (Mother)",
                            iconClass = "",
                            path = "/Financial/OtherChildren/" + userId,
                            itemClass = ""
                        },
                    new MenuItem
                        {
                            formName = "Deviations",
                            text = "Deviations",
                            iconClass = "",
                            path = "/Financial/Deviations/" + userId,
                            itemClass = ""
                        },
                };
            return new MenuItem
            {
                itemClass = "submenu",
                path = "",
                iconClass = "icon icon-th-list",
                text = "Mediation Agreement",
                showSubMenu = false,
                subMenuItems = menuList
            };

        }
        #endregion
    }
}
