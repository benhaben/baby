﻿// Decompiled with JetBrains decompiler
// Type: Aliyun.OpenServices.OpenStorageService.Transform.SetBucketWebsiteRequestSerializer
// Assembly: Aliyun.OpenServices, Version=1.0.5290.21916, Culture=neutral, PublicKeyToken=0ad4175f0dac0b9b
// MVID: 6C2A4E91-5F65-4F0D-B29C-34B3D99D5AA0
// Assembly location: Y:\Downloads\aliyun_dotnet_sdk_20140626 (1)\bin\Aliyun.OpenServices.dll

using Aliyun.OpenServices.Common.Transform;
using Aliyun.OpenServices.OpenStorageService;
using Aliyun.OpenServices.OpenStorageService.Model;
using System.IO;

namespace Aliyun.OpenServices.OpenStorageService.Transform
{
  internal class SetBucketWebsiteRequestSerializer : RequestSerializer<SetBucketWebsiteRequest, SetBucketWebsiteRequestModel>
  {
    public SetBucketWebsiteRequestSerializer(ISerializer<SetBucketWebsiteRequestModel, Stream> contentSerializer)
      : base(contentSerializer)
    {
    }

    public override Stream Serialize(SetBucketWebsiteRequest request)
    {
      SetBucketWebsiteRequestModel input = new SetBucketWebsiteRequestModel();
      input.ErrorDocument = new SetBucketWebsiteRequestModel.ErrorDocumentModel();
      input.IndexDocument = new SetBucketWebsiteRequestModel.IndexDocumentModel();
      input.IndexDocument.Suffix = request.IndexDocument;
      input.ErrorDocument.Key = request.ErrorDocument;
      return this.ContentSerializer.Serialize(input);
    }
  }
}
