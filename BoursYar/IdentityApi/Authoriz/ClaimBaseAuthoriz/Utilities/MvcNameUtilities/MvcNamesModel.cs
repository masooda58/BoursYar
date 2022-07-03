﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Authoriz.Utilities.MvcNameUtilities
{
    public class MvcNamesModel:IEquatable<MvcNamesModel>
    {
        public string AreaName { get;  }
        public string ControllerName { get;  }
        public string ActionName { get;  }
        public string ClaimToAuthoriz { get;  }
        public bool IsClaimBaseAuthoraztionRequired { get;  }

        public MvcNamesModel( string claimToAuthoriz, string actionName, string controllerName, string areaName)
        {
            IsClaimBaseAuthoraztionRequired = !string.IsNullOrWhiteSpace(claimToAuthoriz);
            ClaimToAuthoriz = claimToAuthoriz;
            ActionName = actionName;
            ControllerName = controllerName;
            AreaName = areaName;
        }

        public MvcNamesModel(string actionName, string controllerName, string areaName)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            AreaName = areaName;
            IsClaimBaseAuthoraztionRequired = false;
        }
        //in regen baray gerftan etlaat dakhel hasset tavasot GetTryValue  ئه ذشساشی
        #region GetHashset

        public bool Equals(MvcNamesModel other)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(other, null)) return false;

            // Optimization for a common success case.
            if (ReferenceEquals(this, other)) return true;

            // If run-time types are not exactly the same, return false.
            if (GetType() != other.GetType()) return false;

            return AreaName == other.AreaName
                   && ControllerName == other.ControllerName
                   && ActionName == other.ActionName;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MvcNamesModel);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AreaName, ControllerName, ActionName);
        }

        #endregion

    }
}