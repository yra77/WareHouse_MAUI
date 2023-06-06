

using WareHouse_MAUI.Enums;


namespace WareHouse_MAUI.Services.AccessRoles
{
    internal class AccessRole : IAccessRole
    {


        private AccessPermisions _permission;


        ///<summary>
        ///        admin permission Highest - full access 
        ///        storekeeper permission Medium - change products, clients, shippers
        ///        seller permission Low - change products
        ///        other Lowest - nothing        
        /// </summary>
        /// <param name="role"></param>

        public void SetRole(string role)
        {
            switch (role)
            {
                case "admin":
                    _permission = AccessPermisions.Highest;
                    break;

                case "storekeeper":
                    _permission = AccessPermisions.Medium;
                    break;

                case "seller":
                    _permission = AccessPermisions.Low;
                    break;

                default:
                    _permission = AccessPermisions.Lowest;
                    break;
            }
        }

        public void GetAccessPermission(out bool highest, out bool medium, out bool low, out bool lowest)
        {
            switch (_permission)
            {
                case AccessPermisions.Highest:
                    highest = true;
                    medium = true;
                    low = true;
                    lowest = true;
                    break;

                case AccessPermisions.Medium:
                    highest = false;
                    medium = true;
                    low = true;
                    lowest = true;
                    break;

                case AccessPermisions.Low:
                    highest = false;
                    medium = false;
                    low = true;
                    lowest = true;
                    break;

                default:
                    highest = false;
                    medium = false;
                    low = false;
                    lowest = false;
                    break;
            }
        }

    }
}
