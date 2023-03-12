using Breezee.Core;
using Breezee.Core.Interface;
using System;

/*********************************************************************	
 * 对象名称：	
 * 对象类别：类	
 * 创建作者：黄国辉	
 * 创建日期：2022/11/14 23:28:19	
 * 对象说明：	
 * 电邮地址: guo7892000@126.com	
 * 微信号: BreezeeHui	
 * 修改历史：	
 *      2022/11/14 23:28:19 新建 黄国辉 	
 * ******************************************************************/
namespace Breezee.Framework.Mini.Entity
{
    /// <summary>
    /// 类
    /// </summary>
    public class MiniApp : FormApp
    {
        private IConfig _config = new MiniConfig();
        private IForm _loginForm;
        private IForm _mainForm;

        public override IConfig Config { get => _config; set => _config = value; }       

        public override IMenu Menu => throw new NotImplementedException();

        public override IForm LoginForm { get => _loginForm; set => _loginForm = value; }
        public override IForm MainForm { get => _mainForm; set => _mainForm=value; }

        public override void Init()
        {
            _config.Init();

        }
    }
}