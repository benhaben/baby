﻿/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 * 版权所有 （C）阿里云计算有限公司
 */

using System;
using Aliyun.OpenServices.Common.Communication;
using Aliyun.OpenServices.Properties;
using Aliyun.OpenServices.OpenStorageService.Utilities;
using System.Collections.Generic;

namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    /// <summary>
    /// Description of DeleteBucketLoggingCommand.
    /// </summary>
    internal class DeleteBucketLoggingCommand  : OssCommand
    {
        private readonly string _bucketName;

        protected override HttpMethod Method
        {
            get { return HttpMethod.Delete; }
        }

        protected override string Bucket
        {
            get
            {
                return _bucketName;
            }
        }

        private DeleteBucketLoggingCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                    string bucketName)
            : base(client, endpoint, context)
        {
            if (string.IsNullOrEmpty(bucketName))
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "bucketName");
            if (!OssUtils.IsBucketNameValid(bucketName))
                throw new ArgumentException(OssResources.BucketNameInvalid, "bucketName");

            _bucketName = bucketName;
        }

        public static DeleteBucketLoggingCommand Create(IServiceClient client, Uri endpoint,
                                                 ExecutionContext context,
                                                 string bucketName)
        {
            return new DeleteBucketLoggingCommand(client, endpoint, context, bucketName);
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "logging", null }
                };
            }
        }
    }
}
