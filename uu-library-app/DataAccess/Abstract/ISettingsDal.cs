﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface ISettingsDal
    {
        Settings getSettings();
        void Update(Settings settings);
        void AddOrUpdate(Settings settings);
    }
}
