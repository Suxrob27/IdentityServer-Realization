namespace IdentityServer.Model.Role
{
    public class RoleViewsModel
    {
        public ApplicationUser User { get; set; }
        public List<RoleModel> Roles { get; set; }

    }
    public class  RoleModel
    {
        public string RoleName { get; set; }    
        public bool IsRoleSelected { get; set; }  
    }
}
