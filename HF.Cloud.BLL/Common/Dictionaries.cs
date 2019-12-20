using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HF.Cloud.BLL.Common
{
    public class Dictionaries
    {
        /// <summary>
        /// 获取DictionaryType 获取字典编码
        /// 字典类型编码需使用数字编码，最长四位
        /// </summary>
        /// <param name="dicType">DictionaryType</param>
        /// <returns></returns>
        public static string GetDictionaryCode(DictionaryType dicType)
        {
            string typeCode = string.Empty;

            typeCode = ((long)dicType).ToString().PadLeft(4, '0');

            return typeCode;
        }



    }

    /// <summary>
    /// 字典枚举
    /// 数据库中最长四位，比如：0001,0002,0130等
    /// </summary>
    public enum DictionaryType
    {
        /// <summary>
        /// 工单状态
        /// </summary>
        SheetStatus = 1,

        /// <summary>
        /// 工单可见范围
        /// </summary>
        SheetVisibleRanage = 2,

        /// <summary>
        /// 工单优先级
        /// </summary>
        SheetPriority = 3,

        /// <summary>
        /// 附件类型
        /// </summary>
        AnnexType = 4,

        /// <summary>
        /// 消息类型
        /// </summary>
        MessageType = 5,
        /// <summary>
        /// 维修任务状态
        /// </summary>
        RepairTaskStatus = 6
    }
    /// <summary>
    /// 维修任务状态
    /// </summary>
    public enum RepairTaskStatus : int
    {
        /// <summary>
        /// 未受理
        /// </summary>
        未受理 = 1,
        /// <summary>
        /// 已受理
        /// </summary>
        已受理 = 2,
        /// <summary>
        /// 处理中
        /// </summary>
        处理中 = 3,
        /// <summary>
        /// 暂停
        /// </summary>
        暂停 = 4,
        /// <summary>
        /// 已完成
        /// </summary>
        已完成 = 5,
        /// <summary>
        /// 已关闭
        /// </summary>
        已关闭 = 6
    }
    /// <summary>
    /// 充值支付方式
    /// </summary>
    public enum PayType : int
    {
        支付宝 = 1,
        微信 = 2,
        系统代充 = 3
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType : int
    {
        工单 = 1,
        维修 = 2
    }

    /// <summary>
    /// 过滤器类型
    /// </summary>
    public enum SelectType : int
    {
        工单 = 1,
        客户 = 2,
        设备 = 3,
        维修 = 4
    }
}
