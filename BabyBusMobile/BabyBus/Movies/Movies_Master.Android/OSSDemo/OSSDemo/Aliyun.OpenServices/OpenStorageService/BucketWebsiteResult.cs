﻿// Decompiled with JetBrains decompiler
// Type: Aliyun.OpenServices.OpenStorageService.BucketWebsiteResult
// Assembly: Aliyun.OpenServices, Version=1.0.5290.21916, Culture=neutral, PublicKeyToken=0ad4175f0dac0b9b
// MVID: 6C2A4E91-5F65-4F0D-B29C-34B3D99D5AA0
// Assembly location: Y:\Downloads\aliyun_dotnet_sdk_20140626 (1)\bin\Aliyun.OpenServices.dll

namespace Aliyun.OpenServices.OpenStorageService
{
  /// <summary>
  /// Get Bucket Website 的请求结果
  /// 
  /// </summary>
  public class BucketWebsiteResult
  {
    /// <summary>
    /// 索引页面
    /// 
    /// </summary>
    public string IndexDocument { get; internal set; }

    /// <summary>
    /// 错误页面
    /// 
    /// </summary>
    public string ErrorDocument { get; internal set; }
  }
}