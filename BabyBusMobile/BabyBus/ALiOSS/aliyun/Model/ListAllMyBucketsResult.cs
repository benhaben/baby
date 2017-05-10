﻿/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 * 版权所有 （C）阿里云计算有限公司
 */

using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aliyun.OpenServices.OpenStorageService.Model
{
    /// <summary>
    /// Description of ListBucketResult.
    /// </summary>
    [XmlRoot("ListAllMyBucketsResult")]
    public class ListAllMyBucketsResult
    {
        [XmlElement("Prefix")]
        public string Prefix { get; set; }

        [XmlElement("Marker")]
        public string Marker { get; set; }

        [XmlElement("MaxKeys")]
        public int? MaxKeys { get; set; }

        [XmlElement("IsTruncated")]
        public bool? IsTruncated { get; set; }

        [XmlElement("NextMarker")]
        public string NextMarker { get; set; }

        [XmlElement("Owner")]
        public Owner Owner { get; set; }

        [XmlArrayItem("Bucket")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<BucketModel> Buckets { get; set; }
    }

    [XmlRoot("Bucket")]
    public class BucketModel
    {
        [XmlElement("Location")]
        public string Location { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("CreationDate")]
        public DateTime CreationDate { get; set; }
    }
}
