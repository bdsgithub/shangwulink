using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HF.Cloud.BLL
{
    public static class EnumHelper
    {
        #region 常用字段
        #region 状态
        public enum State
        {
            停用 = 0,
            正常 = 1
        }

        public static string GetState(int State, bool NoHtml = false)
        {
            switch (State)
            {
                case 0:
                    return NoHtml ? "停用" : "<span class=\"label label-default\">停用</span>";
                    break;
                case 1:
                    return NoHtml ? "正常" : "<span class=\"label label-success\">正常</span>";
                    break;
            }
            return NoHtml ? "未知" : "<span class=\"label label-default\">未知</span>";
        }

        #endregion

        #region 删除
        public enum Deleted
        {
            否 = 0,
            是 = 1
        }
        #endregion

        #region 审核
        public enum Audit
        {
            等待审核 = 0,
            通过审核 = 1,
            未通过 = 2
        }


        public static string GetAudit(int Audit, bool NoHtml = false)
        {
            switch (Audit)
            {
                case 0:
                    return NoHtml ? "等待审核" : "<span class=\"label label-default\">等待审核</span>";
                    break;
                case 1:
                    return NoHtml ? "通过审核" : "<span class=\"label label-success\">通过审核</span>";
                    break;
                case 2:
                    return NoHtml ? "未通过" : "<span class=\"label label-warning\">未通过</span>";
                    break;
            }
            return NoHtml ? "未知" : "<span class=\"label label-default\">未知</span>";
        }
        #endregion

        #region 是否
        public enum TrueFalse
        {
            否 = 0,
            是 = 1
        }

        public static string GetTrueFalse(int TrueFalse, bool NoHtml = false)
        {
            switch (TrueFalse)
            {
                case 0:
                    return NoHtml ? "否" : "<span class=\"label label-default\">否</span>";
                    break;
                case 1:
                    return NoHtml ? "是" : "<span class=\"label label-success\">是</span>";
                    break;
            }
            return NoHtml ? "否" : "<span class=\"label label-default\">否</span>";
        }
        #endregion

        #region 开启关闭
        public enum OpenState
        {
            关闭 = 0,
            开启 = 1
        }

        public static string GetOpenState(int OpenState, bool NoHtml = false)
        {
            switch (OpenState)
            {
                case 0:
                    return NoHtml ? "关闭" : "<span class=\"label label-danger\">关闭</span>";
                    break;
                case 1:
                    return NoHtml ? "开启" : "<span class=\"label label-success\">开启</span>";
                    break;
            }
            return NoHtml ? "关闭" : "<span class=\"label label-danger\">关闭</span>";
        }
        #endregion

        #region 显示隐藏
        public enum ShowState
        {
            隐藏 = 0,
            显示 = 1
        }

        public static string GetShowState(int ShowState, bool NoHtml = false)
        {
            switch (ShowState)
            {
                case 0:
                    return NoHtml ? "隐藏" : "<span class=\"label label-default\">隐藏</span>";
                    break;
                case 1:
                    return NoHtml ? "显示" : "<span class=\"label label-success\">显示</span>";
                    break;
            }
            return NoHtml ? "隐藏" : "<span class=\"label label-default\">隐藏</span>";
        }
        #endregion

        #region 性别
        public enum Sex
        {
            女 = 0,
            男 = 1
        }

        public static string GetSex(int Sex, bool NoHtml = false)
        {
            switch (Sex)
            {
                case 0:
                    return NoHtml ? "女" : "<span class=\"label label-warning\">女</span>";
                    break;
                case 1:
                    return NoHtml ? "男" : "<span class=\"label label-success\">男</span>";
                    break;
            }
            return NoHtml ? "未知" : "<span class=\"label label-default\">未知</span>";
        }
        #endregion
        #endregion

        #region 系统专用

        #region 短信验证类型
        public enum MsgType
        {
            注册 = 100,
            登录 = 101,
            修改密码 = 102
        }
        #endregion

        #region 用户信息类型
        public enum UserInfoType
        {
            昵称 = 0,
            头像 = 1,
            签名 = 2,
            学校 = 3,
            定位学校 = 4
        }
        #endregion

        #region 上传图片类型

        public enum UpClientType
        {
            其他 = 0,
            管理 = 1,
            APP = 2
        }

        public static string GetUpClientTypeDir(int ImageType)
        {
            string BackStr = "Other";
            switch (ImageType)
            {
                case 1:
                    BackStr = "Market";
                    break;
                case 2:
                    BackStr = "School";
                    break;
            }
            return BackStr;
        }

        public enum UpImageType
        {
            其他 = 0,
            头像 = 1,
            产品 = 2,
            广告 = 3
        }

        public static string GetUpImageDir(int ImageType)
        {
            string BackStr = "Other";
            switch (ImageType)
            {
                case 1:
                    BackStr = "Headimg";
                    break;
                case 2:
                    BackStr = "Product";
                    break;
                case 3:
                    BackStr = "advert";
                    break;
            }
            return BackStr;
        }
        #endregion

        #region 订单

        #region 支付类型

        #endregion
        public enum PayType
        {
            在线支付 = 1,
            货到付款 = 2
        }

        public static string GetPayType(int PayType, bool NoHtml = false)
        {
            switch (PayType)
            {
                case 1:
                    return NoHtml ? "在线支付" : "<span class=\"label label-success\">在线支付</span>";
                    break;
                case 2:
                    return NoHtml ? "货到付款" : "<span class=\"label label-info\">货到付款</span>";
                    break;
            }
            return NoHtml ? "未知" : "<span class=\"label label-default\">未知</span>";
        }

        public enum PayState
        {
            待付 = 0,
            已付 = 1,
            付款返回未成功 = 2
        }

        public enum OrderStep
        {
            待付款 = 0,
            已付款 = 1,
            已发货 = 2,
            已签收 = 3,
            退货中 = 4,
            已完成 = 5
        }

        public static string GetOrderStep(int Step, bool NoHtml = false)
        {
            switch (Step)
            {
                case 0:
                    return NoHtml ? "待付款" : "<span class=\"label label-info\">待付款</span>";
                    break;
                case 1:
                    return NoHtml ? "已付款" : "<span class=\"label label-info\">已付款</span>";
                    break;
                case 2:
                    return NoHtml ? "已发货" : "<span class=\"label label-info\">已发货</span>";
                    break;
                case 3:
                    return NoHtml ? "已签收" : "<span class=\"label label-info\">已签收</span>";
                    break;
                case 4:
                    return NoHtml ? "退货中" : "<span class=\"label label-warning\">退货中</span>";
                    break;
                case 5:
                    return NoHtml ? "已完成" : "<span class=\"label label-success\">已完成</span>";
                    break;
            }
            return NoHtml ? "未知" : "<span class=\"label label-default\">未知</span>";
        }

        public enum OrderState
        {
            已取消 = 0,
            正常订单 = 1,
            已完成 = 2
        }
        #endregion

        public enum RoleType
        {
            用户 = 1,
            商家 = 2,
            超管 = 9
        }

        #endregion

        #region 工单优先级
        public static string GetSheetLevel(string Level, bool NoHtml = false)
        {
            if (Level == "1")
            {
                return NoHtml ? "紧急" : "<span class=\"label label-y\">紧急</span>";
            }
            else if (Level == "2")
            {
                return NoHtml ? "高" : "<span class=\"label label-default\">高</span>";
            }
            else if (Level == "3")
            {
                return NoHtml ? "标准" : "<span class=\"label label-warning\">标准</span>";
            }
            else if (Level == "4")
            {
                return NoHtml ? "低" : "<span class=\"label label-success\">低</span>"; 
            }
            else
            {
                return NoHtml ? "未知" : "<span class=\"label label-warning\">未知</span>";
            }

        }

        #endregion

        #region 工单状态
        public static string GetSheetState(string state, bool NoHtml = false)
        {
            if (state == "1")
            {
                return NoHtml ? "未受理" : "<span class=\"label label-wsl\">未受理</span>";
            }
            else if (state == "2")
            {
                return NoHtml ? "已受理" : "<span class=\"label label-ysl\">已受理</span>";
            }
            else if (state == "3")
            {
                return NoHtml ? "处理中" : "<span class=\"label label-clz\">处理中</span>";
            }
            else if (state == "4")
            {
                return NoHtml ? "暂停中" : "<span class=\"label label-ztz\">暂停中</span>";
            }
            else if (state == "5")
            {
                return NoHtml ? "已删除" : "<span class=\"label label-ysc\">已删除</span>";
            }
            else if (state == "6")
            {
                return NoHtml ? "已完成" : "<span class=\"label label-ywc\">已完成</span>";
            }
            else if (state == "7")
            {
                return NoHtml ? "已关闭" : "<span class=\"label label-ywc\">已关闭</span>";
            }
            else
            {
                return NoHtml ? "未知" : "<span class=\"label label-warning\">未知</span>";
            }

        }


        #endregion

        #region 获取可见范围
        public static string Look(string lk)
        {
            if (lk=="1")
            {
                return "全部访问";
            } else if (lk=="2")
            {
                return "组访问";
            }
            else if (lk=="3")
            {
                return "特定访问";
            }
            else
            {
                return "访问权限貌似出错了";

            }

        }


        #endregion

        #region 是否查看
        public static string GetSee(string state, bool NoHtml = false)
        {
            if (state == "1")
            {
                return NoHtml ? "已查看" : "<span class=\"label label-success\">已查看</span>";
            }
            else 
            {
                return NoHtml ? "未查看" : "<span class=\"label label-warning\">未查看</span>";
            }
        }

        #endregion 


    }
}
