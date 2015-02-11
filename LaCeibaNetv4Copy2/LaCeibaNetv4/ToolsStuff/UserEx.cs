using LaCeibaNetv4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaCeibaNetv4.ToolsStuff
{
    //This class if for user extention methods
    public static class UserEx
    {
        public static string GetFullName(this ClientsTbl client) {

            return client.FirstName + " " + client.MiddleName1 + " " + client.MiddleName2 + " " + client.LastName;
        }
    }
}