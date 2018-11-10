using GDIC;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVM.Src.Components
{
    public class Storage
    {
        private readonly Dictionary<Registers, object> data;
        private readonly ServiceCollection services;
        public Storage(ServiceCollection service)
        {
            data = new Dictionary<Registers, object>();
            services = service;
        }

    }
}
