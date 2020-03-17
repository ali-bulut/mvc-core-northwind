using Core.Entitites.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product was successfully added!";
        public static string ProductUpdated = "Product was successfully updated!";
        public static string ProductDeleted = "Product was successfully deleted!";
        public static string UserNotFound = "User couldn't be found!";
        public static string PasswordError = "Your password is wrong!";
        public static string SuccessfulLogin = "Login process was successfully completed!";
        public static string UserAlreadyExist = "User already exist!";
        public static string UserRegistered = "Register process was successfully completed!";
        public static string AccessTokenCreated = "Access Token was successfully created!";
        public static string AuthorizationDenied = "You are not authorized to access here!";
        public static string ProductNameAlreadyExists = "Product Name already exists!";
    }
}
