using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DClean.Application.Interfaces.Identity;
using DClean.Application.Interfaces.Services;

namespace DClean.WebApi.Services
{
    public class CurrentTenant : ICurrentTenant, IDisposable, IAsyncDisposable
    {
        private class TenantHolder
        {
            public Guid? TenantId { get; set; }
        }
        private static readonly AsyncLocal<TenantHolder> _currentTenant = new AsyncLocal<TenantHolder>();
        //private Guid? _tenantId;
        public string TenantIdString { get; private set; }
        private string tenantName = null;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public Guid? TenantId
        {
            get
            {
                if (_currentTenant.Value?.TenantId.HasValue ?? false) return _currentTenant.Value.TenantId;
                if (string.IsNullOrEmpty(TenantIdString)) return null;
                var canParse = Guid.TryParse(TenantIdString, out var value);
                if (!canParse) throw new InvalidCastException("invalid tenant id");
                _currentTenant.Value.TenantId = value;
                return _currentTenant.Value.TenantId;
            }
        }

        public CurrentTenant(IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            IServiceProvider serviceProvider
            )
        {
            TenantIdString = httpContextAccessor.HttpContext?.User?.FindFirstValue("tid");
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public Guid? GetTenantId()
        {
            if (TenantId == null) throw new ArgumentNullException(nameof(TenantId), "tenantId can't be null");
            return TenantId.Value;
        }

        public async Task<string> GetTenantNameAsync()
        {
            var tenantService = _serviceProvider.GetRequiredService<ITenantService>();

            if (tenantName != null) return tenantName;
            if (!TenantId.HasValue) return "Main";
            var tenant = await tenantService.GetAsync(TenantId.Value);
            return tenant.Name;
        }

        public async Task<string> GetConnectionStringAsync(string name = "DefaultConnection")
        {
            string connectionStr = null;
            if (TenantId.HasValue)
            {
                var tenantConnectionStringService = _serviceProvider.GetRequiredService<ITenantConnectionStringService>();
                connectionStr = await tenantConnectionStringService.GetByTenantIdAsync(TenantId.Value, name);
                if (!string.IsNullOrEmpty(connectionStr)) return connectionStr;
            }
            connectionStr = _configuration.GetConnectionString(name);
            return connectionStr;
        }

        public void Dispose()
        {
            tenantName = null;
            Use(null);
        }

        public IDisposable Use(Guid? tenantId)
        {
            var holder = _currentTenant;
            if (holder != null)
            {
                holder.Value = null;
            }
            if (tenantId != null)
            {
                _currentTenant.Value = new TenantHolder() { TenantId = tenantId };
            }
            return this;
        }
        public async Task<IAsyncDisposable> UseAsync(Guid? tenantId)
        {
            var holder = _currentTenant;
            if (holder != null)
            {
                holder.Value = null;
            }
            if (tenantId != null)
            {
                _currentTenant.Value = new TenantHolder() { TenantId = tenantId };
            }
            return this;
        }

        public async ValueTask DisposeAsync()
        {
            //tenantName = null;
            //_currentTenant.Value.TenantId = null;
        }

        public Task<IAsyncDisposable> UseAsync(Guid tenantId)
        {
            throw new NotImplementedException();
        }

        public IDisposable Use(Guid tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
