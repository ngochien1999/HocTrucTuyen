using System;

namespace FisrtApplication
{
    public interface IApplicationBuilder
    {
        void UseDeveloperExceptionPage();
        void UseRouting();
        void UseEndpoints(Action<object> p);
    }
}