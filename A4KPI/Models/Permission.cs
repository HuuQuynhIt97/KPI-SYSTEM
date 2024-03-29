﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A4KPI.Models
{
    public class Permission
    {
        public Permission()
        {
        }

        public Permission(int roleID, int optionID, int functionSystemID)
        {
            RoleID = roleID;
            OptionID = optionID;
            FunctionSystemID = functionSystemID;
        }

        public int RoleID { get; set; }
        public int OptionID { get; set; }
        public int FunctionSystemID { get; set; }
        public virtual FunctionSystem Functions { get; set; }
        public virtual Role Role { get; set; }
        public virtual Option Option { get; set; }
    }
}
