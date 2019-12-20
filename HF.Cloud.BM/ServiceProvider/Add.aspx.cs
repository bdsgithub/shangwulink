using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HF.Cloud.BLL;
using HF.Cloud.Model;
using System.Transactions;
using HF.Cloud.BLL.Common;

namespace HF.Cloud.BM.ServiceProvider
{
    public partial class Add : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnSearch_Click(object sender, EventArgs e)
        {
            string tips = ValidateInput();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (tips == string.Empty)
                    {
                        SysBL bl = new SysBL();
                        SB_UserEL el = new SB_UserEL();

                        BLL.Common.Symmetric sm = new BLL.Common.Symmetric();

                        el = bl.InsertUser(this.txtUserCode.Text.Trim(), sm.Encrypto("123456"), this.txtUserCode.Text.Trim(), this.txtSBName.Text.Trim(), txtUserName.Text.Trim());

                        bl.addQBCodeModel(el.MainID);//给服务商添加一个默认模板
                        //给服务商添加一个支付账号，开通账户
                        string Cname = this.txtSBName.Text.Trim();//企业名称
                        bl.addMainAccount(el.MainID, Cname);
                        //给服务商添加短信模板
                        bl.addMessageSet(el.MainID);

                        if (el != null && el.ID > 0)
                        {
                            MessageBox.ShowAndRedirect(this, "服务商保存成功，默认密码为：123456", "/ServiceProvider/index.aspx");
                            scope.Complete();
                        }
                        else
                        {
                            MessageBox.Show(this, "增加用户账户时失败！");
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, tips);
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(this, "保存失败，" + er.Message + "！");
                }
            }
        }

        protected string ValidateInput()
        {
            string tips = string.Empty;

            if (this.txtSBName.Text.Trim() == string.Empty)
            {
                tips += "企业名称不能为空！\\n";
            }
            else
            {
                //SB_UserMEL userM = new SB_UserMEL()
                //{
                //    SBName = this.txtSBName.Text.Trim()
                //};

                //userM.ExecuteEL(43);

                //if (!string.IsNullOrEmpty(userM.ApplyIP))
                //{
                //    tips += "企业名称已经注册，不能重复注册！\\n";
                //}
            }

            if (this.txtUserCode.Text.Trim() != string.Empty)
            {
                if (PageValidate.IsPhone(this.txtUserCode.Text.Trim()))
                {
                    SB_UserMEL userM = new SB_UserMEL()
                    {
                        SBMobile = this.txtUserCode.Text.Trim()
                    };

                    userM.ExecuteEL(44);

                    if (!string.IsNullOrEmpty(userM.ApplyIP))
                    {
                        tips += "手机号已在服务商注册，不能重复注册！\\n";
                    }

                    SB_UserEL user = new SB_UserEL() { UserCode = this.txtUserCode.Text.Trim() };

                    user.ExecuteEL(41);

                    if (!string.IsNullOrEmpty(user.UserName))
                    {
                        tips += "手机号已作为账户被注册过，不能重复注册！\\n";
                    }
                }
                else
                {
                    tips += "手机号格式输入不正确，请重新输入！\\n";
                }
            }
            else
            {
                tips += "手机号不能为空！\\n";
            }

            if (this.txtUserName.Text.Trim() == string.Empty)
            {
                tips += "管理员姓名不能为空！";
            }

            return tips;
        }

        protected void lnkBtnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}