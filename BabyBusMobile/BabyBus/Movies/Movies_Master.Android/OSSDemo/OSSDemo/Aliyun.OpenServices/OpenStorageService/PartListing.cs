﻿// Decompiled with JetBrains decompiler
// Type: Aliyun.OpenServices.OpenStorageService.PartListing
// Assembly: Aliyun.OpenServices, Version=1.0.5290.21916, Culture=neutral, PublicKeyToken=0ad4175f0dac0b9b
// MVID: 6C2A4E91-5F65-4F0D-B29C-34B3D99D5AA0
// Assembly location: Y:\Downloads\aliyun_dotnet_sdk_20140626 (1)\bin\Aliyun.OpenServices.dll

using System.Collections.Generic;

namespace Aliyun.OpenServices.OpenStorageService
{
  /// <summary>
  /// 获取List Parts的结果.
  /// 
  /// </summary>
  public class PartListing
  {
    private IList<Part> _parts = (IList<Part>) new List<Part>();

    /// <summary>
    /// 获取Object所在的<see cref="T:Aliyun.OpenServices.OpenStorageService.Bucket"/>的名称。
    /// 
    /// </summary>
    public string BucketName { get; internal set; }

    /// <summary>
    /// 获取<see cref="T:Aliyun.OpenServices.OpenStorageService.OssObject"/>的名称。
    /// 
    /// </summary>
    public string Key { get; internal set; }

    /// <summary>
    /// 获取请求参数<see cref="P:ListPartsRequest.UploadId"/>的值。
    /// 
    /// </summary>
    public string UploadId { get; internal set; }

    /// <summary>
    /// 获取请求参数<see cref="P:ListPartsRequest.PartNumberMarker"/>的值。
    /// 
    /// </summary>
    public int PartNumberMarker { get; internal set; }

    /// <summary>
    /// 如果本次没有返回全部结果，响应请求中将包含NextPartNumberMarker元素，
    ///             用于标明接下来请求的PartNumberMarker值。
    /// 
    /// </summary>
    public int NextPartNumberMarker { get; internal set; }

    /// <summary>
    /// 获取请求参数<see cref="P:ListPartsRequest.MaxParts"/>的值。
    /// 
    /// </summary>
    public int MaxParts { get; internal set; }

    /// <summary>
    /// 标明是否本次返回的List Part结果列表被截断。
    ///             “true”表示本次没有返回全部结果；“false”表示本次已经返回了全部结果。
    /// 
    /// </summary>
    public bool IsTruncated { get; internal set; }

    /// <summary>
    /// 获取所有的Part
    /// 
    /// </summary>
    public IEnumerable<Part> Parts
    {
      get
      {
        return (IEnumerable<Part>) this._parts;
      }
    }

    internal void AddPart(Part part)
    {
      this._parts.Add(part);
    }
  }
}
