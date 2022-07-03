﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using IdentityApi.Authoriz.ClaimBaseAuthoriz.Attribute;
using IdentityApi.Authoriz.Utilities.MvcNameUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace IdentityApi.Authoriz.ClaimBaseAuthoriz.Utilities.MvcNameUtilities
{
    public class MvcUtilities:IMvcUtilities
    {
        public MvcUtilities(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            var mvcInfo = new List<MvcNamesModel>();
            var actionThatRequireClaimBaseAuthorazition = new List<MvcNamesModel>();
            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach (var Descriptor in actionDescriptors)
            {
                if (!(Descriptor is ControllerActionDescriptor controllerActionDescriptor))  continue;
                var controllerTypeInfo = controllerActionDescriptor.ControllerTypeInfo;
                var claimToAuthoriz = controllerActionDescriptor.MethodInfo
                    .GetCustomAttribute<ClaimBaseAuthorizAttribute>()?.ClaimToAuthoriz;
                mvcInfo.Add(new MvcNamesModel(
                    areaName:controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                   controllerName: controllerActionDescriptor.ControllerName,
                    actionName:controllerActionDescriptor.ActionName,
                    claimToAuthoriz:claimToAuthoriz
                    ));
                if (!string.IsNullOrWhiteSpace(claimToAuthoriz))
                {
                    actionThatRequireClaimBaseAuthorazition.Add(new MvcNamesModel(
                       areaName:controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                       controllerName:controllerActionDescriptor.ControllerName,
                       actionName:controllerActionDescriptor.ActionName,
                       claimToAuthoriz:claimToAuthoriz
                        ));
                }

            }

            MvcInfo = ImmutableHashSet.CreateRange(mvcInfo);
            ActionThatRequireClaimBaseAuthorazition =
                ImmutableHashSet.CreateRange(actionThatRequireClaimBaseAuthorazition);
        }
        public ImmutableHashSet<MvcNamesModel> MvcInfo { get; }
        public ImmutableHashSet<MvcNamesModel> ActionThatRequireClaimBaseAuthorazition { get; }
    }
}