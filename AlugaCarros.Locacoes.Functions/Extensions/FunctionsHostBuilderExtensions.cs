using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlugaCarros.Locacoes.Functions.Extensions;
public static class FunctionsHostBuilderExtensions
{
    public static TService GetService<TService>(this IFunctionsHostBuilder builder)
        => builder.Services.BuildServiceProvider().GetRequiredService<TService>();

}

