﻿using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

namespace ProcessMeme.Extensions
{
	public static class FunctionsHostBuilderExtensions
    {
		public static bool IsProduction(this IFunctionsHostBuilder builder)
			=> builder.GetContext().EnvironmentName.Equals("Production", StringComparison.OrdinalIgnoreCase);
	}
}

