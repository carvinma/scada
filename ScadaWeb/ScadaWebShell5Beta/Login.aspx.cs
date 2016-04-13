﻿/*
 * Copyright 2016 Mikhail Shiryaev
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : SCADA-Web
 * Summary  : Login web form
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2016
 * Modified : 2016
 */

using Scada.UI;
using System;

namespace Scada.Web
{
    /// <summary>
    /// Login web form
    /// <para>Веб-форма входа в систему</para>
    /// </summary>
    public partial class WFrmLogin : System.Web.UI.Page
    {
        /// <summary>
        /// Добавить на страницу скрипт вывода сообщения об ошибке
        /// </summary>
        private void AddShowAlertScript(string message)
        {
            ClientScript.RegisterStartupScript(GetType(), "Startup", "showAlert('" + message + "');", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Title = (string)ViewState["Title"];
            }
            else
            {
                // перевод веб-страницы
                Translator.TranslatePage(this, "Scada.Web.WFrmLogin");
                ViewState["Title"] = Title;

                UserData userData = UserData.GetUserData();
                pnlRememberMe.Visible = userData.WebSettings.RemEnabled;

                if (userData.LoggedOn)
                {
                    txtUsername.Text = userData.UserName;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserData userData = UserData.GetUserData();
            string errMsg;
            if (userData.Login(txtUsername.Text, txtPassword.Text, out errMsg))
                Response.Redirect("~/View.aspx");
            else
                AddShowAlertScript(errMsg);
        }
    }
}