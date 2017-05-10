﻿// Decompiled with JetBrains decompiler
// Type: Aliyun.OpenServices.ServiceException
// Assembly: Aliyun.OpenServices, Version=1.0.5290.21916, Culture=neutral, PublicKeyToken=0ad4175f0dac0b9b
// MVID: 6C2A4E91-5F65-4F0D-B29C-34B3D99D5AA0
// Assembly location: Y:\Downloads\aliyun_dotnet_sdk_20140626 (1)\bin\Aliyun.OpenServices.dll

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Aliyun.OpenServices
{
  /// <summary>
  /// 
  /// <para>
  /// 表示阿里云服务返回的错误消息。
  /// 
  /// </para>
  /// 
  /// <para>
  /// <see cref="T:Aliyun.OpenServices.ServiceException"/>用于处理阿里云服务返回的错误消息。
  ///             比如，用于身份验证的AccessKeyID不存在，则会抛出<see cref="T:Aliyun.OpenServices.ServiceException"/>。
  ///             （严格上讲，会是该类的一个继承类。比如，OTS服务会抛出<see cref="C:Aliyun.OpenServices.OpenTableService.OtsException"/>）。
  ///             异常中包含了错误代码，用于让调用者进行特定的处理。
  /// 
  /// </para>
  /// 
  /// <para>
  /// <see cref="T:System.Net.WebException"/>表示的则是在向阿里云服务发送请求时出现的错误。
  ///             例如，在发送请求时网络连接不可用，则会抛出<see cref="T:System.Net.WebException"/>的异常。
  /// 
  /// </para>
  /// 
  /// <para>
  /// <see cref="T:System.InvalidOperationException"/>表示客户端代码无法处理或解析返回结果。这种情况
  ///             有可能是服务器返回的结果不完整，或不再符合SDK设计时的规范。如果持续遇到该错误，请考虑升级
  ///             SDK版本，或联系我们。
  /// 
  /// </para>
  /// 
  ///             通常来讲，调用者只需要处理<see cref="T:Aliyun.OpenServices.ServiceException"/>。因为该异常表明请求被服务处理，
  ///             但处理的结果表明存在错误。异常中包含了细节的信息，特别是错误代码，可以帮助调用者进行处理。
  /// 
  /// <para/>
  /// 
  /// </summary>
  [Serializable]
  public class ServiceException : Exception
  {
    /// <summary>
    /// 获取错误代码。
    /// 
    /// </summary>
    public virtual string ErrorCode { get; internal set; }

    /// <summary>
    /// 获取Request ID。
    /// 
    /// </summary>
    public virtual string RequestId { get; internal set; }

    /// <summary>
    /// 获取Host ID。
    /// 
    /// </summary>
    public virtual string HostId { get; internal set; }

    /// <summary>
    /// 初始化新的<see cref="T:Aliyun.OpenServices.ServiceException"/>实例。
    /// 
    /// </summary>
    public ServiceException()
    {
    }

    /// <summary>
    /// 初始化新的<see cref="T:Aliyun.OpenServices.ServiceException"/>实例。
    /// 
    /// </summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    public ServiceException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// 初始化新的<see cref="T:Aliyun.OpenServices.ServiceException"/>实例。
    /// 
    /// </summary>
    /// <param name="message">解释异常原因的错误信息。</param><param name="innerException">导致当前异常的异常。</param>
    public ServiceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// 初始化新的<see cref="T:Aliyun.OpenServices.ServiceException"/>实例。
    /// 
    /// </summary>
    /// <param name="info">保存序列化对象数据的对象。</param><param name="context">有关源或目标的上下文信息。</param>
    protected ServiceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    /// 重载<see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)"/>方法。
    /// 
    /// </summary>
    /// <param name="info"><see cref="T:System.Runtime.Serialization.SerializationInfo"/>，它存有有关所引发异常的序列化的对象数据。</param><param name="context"><see cref="T:System.Runtime.Serialization.StreamingContext"/>，它包含有关源或目标的上下文信息。</param>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}
