﻿namespace IdentityServer.Model
{
    public class TwoFactorAuthenticationViewModel
    {
        public string Code {  get; set; }   
        public string? Token { get; set; }   
    }
}
