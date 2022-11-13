using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Data.Context;
using Jwt.Identity.Domain.IdentityPolicy.Data;
using Jwt.Identity.Domain.IdentityPolicy.Entity;
using Jwt.Identity.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Jwt.Identity.Data.Repositories.IdentitySettingRepository
{
    public class IdentitySettingRepositoryService:IIdentityPolicyRepository
    {
        private readonly IdentityContext _context;
        private readonly IMemoryCache _memoryCache;
        public IdentitySettingRepositoryService(IdentityContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public IdentitySettingPolicy GetSetting()
        {
            // check memmory cache
           var settingInMemmory= _memoryCache.TryGetValue(CacheKey.IdentitySetting,out IdentitySettingPolicy setting);
           //if Memory cash empty
           if (settingInMemmory)
           {
               return setting;
           }
            var settingInDb = _context.IdentitySettings.AsNoTracking().ToList();
            if (settingInDb.Count == 1)
            {
                _memoryCache.Set(CacheKey.IdentitySetting, settingInDb[0]);
                return settingInDb[0];
            }
          

            throw new Exception("اطلاعات در دیتا بیس دچار مشکل است");

        }

        public void UpdateSetting(IdentitySettingPolicy setting)
        {
            bool isDetached = _context.Entry(setting).State == EntityState.Detached;
            if (isDetached)
              _context.Attach(setting);
            _context.Entry(setting).State = EntityState.Modified;
            // set memmory cache
            _memoryCache.Set(CacheKey.IdentitySetting, setting);

        }

        public bool SettingExist(int id)
        {
            return _context.IdentitySettings.Any(i => i.Id == id);
        }
    }
}
