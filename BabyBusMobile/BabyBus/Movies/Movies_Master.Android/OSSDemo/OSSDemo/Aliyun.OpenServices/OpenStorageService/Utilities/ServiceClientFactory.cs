﻿// Decompiled with JetBrains decompiler
// Type: Aliyun.OpenServices.OpenStorageService.Utilities.ServiceClientFactory
// Assembly: Aliyun.OpenServices, Version=1.0.5290.21916, Culture=neutral, PublicKeyToken=0ad4175f0dac0b9b
// MVID: 6C2A4E91-5F65-4F0D-B29C-34B3D99D5AA0
// Assembly location: Y:\Downloads\aliyun_dotnet_sdk_20140626 (1)\bin\Aliyun.OpenServices.dll

using Aliyun.OpenServices;
using Aliyun.OpenServices.Common.Communication;
using System.Diagnostics;

namespace Aliyun.OpenServices.OpenStorageService.Utilities
{
  /// <summary>
  /// Description of ServiceClientFactory.
  /// 
  /// </summary>
  internal class ServiceClientFactory
  {
    public static IServiceClient CreateServiceClient(ClientConfiguration configuration)
    {
      Debug.Assert(configuration != null);
      return (IServiceClient) new RetryableServiceClient((IServiceClient) ServiceClient.Create(configuration))
      {
        MaxErrorRetry = configuration.MaxErrorRetry
      };
    }
  }
}
