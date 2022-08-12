using BoursYar.Authorization.Attribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace BoursYar.Authorization.Utilities.MvcNameUtilities
{
    /// <summary>
    /// دو تابع دارد 
    /// </summary>
    public class MvcUtilities : IMvcUtilities
    {
        /// <summary>
        /// mvcInfo  , ActionThatRequireClaimBaseAuthorazition
        /// هر دو در سازنده مدل مقدار دهی می شوند
        /// </summary>
        /// <inheritdoc cref="IMvcUtilities"/>
        /// <param name="actionDescriptorCollectionProvider"></param>
        public MvcUtilities(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            var mvcInfo = new List<MvcNamesModel>();
            var actionThatRequireClaimBaseAuthorazition = new List<MvcNamesModel>();
            var actionDescriptors = actionDescriptorCollectionProvider
                .ActionDescriptors.Items;
            foreach (var descriptor in actionDescriptors)
            {
                if (!(descriptor is ControllerActionDescriptor controllerActionDescriptor)) continue;
                var controllerTypeInfo = controllerActionDescriptor.ControllerTypeInfo;
                var claimToAuthoriz = controllerActionDescriptor.MethodInfo
                    .GetCustomAttribute<BoursYarAuthorizAttribute>()?.ClaimToAuthoriz;
                mvcInfo.Add(new MvcNamesModel(
                    areaName: controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                   controllerName: controllerActionDescriptor.ControllerName,
                    actionName: controllerActionDescriptor.ActionName,
                    claimToAuthoriz: claimToAuthoriz
                    ));
                if (!string.IsNullOrWhiteSpace(claimToAuthoriz))
                {
                    actionThatRequireClaimBaseAuthorazition.Add(new MvcNamesModel(
                       areaName: controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                       controllerName: controllerActionDescriptor.ControllerName,
                       actionName: controllerActionDescriptor.ActionName,
                       claimToAuthoriz: claimToAuthoriz
                        ));
                }

            }

            MvcInfo = ImmutableHashSet.CreateRange(mvcInfo);
            ActionThatRequireClaimBaseAuthorazition =
                ImmutableHashSet.CreateRange(actionThatRequireClaimBaseAuthorazition);
        }


        /// <summary>
        /// <inheritdoc cref="IMvcUtilities"/>
        /// </summary>
        public ImmutableHashSet<MvcNamesModel> MvcInfo { get; }
        /// <summary>
        /// <inheritdoc cref="IMvcUtilities"/>
        /// </summary>
        public ImmutableHashSet<MvcNamesModel> ActionThatRequireClaimBaseAuthorazition { get; }
    }
}
