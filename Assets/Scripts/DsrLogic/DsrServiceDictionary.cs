using System;
using System.Collections.Generic;
using DSR.DsrLogic.Services;

namespace DSR.DsrLogic
{
    public class DsrServiceDictionary
    {
        private Dictionary<string, IActionService> _services;

        public DsrServiceDictionary()
        {
            _services = new Dictionary<string, IActionService>();
        }

        public void ConfigureService(string key, IActionService value)
        {
            _services.Add(key, value);
        }

        public IActionService Access(string key)
        {
            var result = _services.TryGetValue(key, out var value);
            if (!result) throw new Exception($"No value associated with key \"{key}\"");

            return value;
        }

    }
}
