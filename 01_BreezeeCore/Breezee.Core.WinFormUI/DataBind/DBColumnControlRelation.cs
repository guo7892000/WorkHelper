using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*********************************************************************		
 * 对象名称：		
 * 对象类别：接口		
 * 创建作者：黄国辉		
 * 创建日期：2022/11/5 22:29:28		
 * 对象说明：		
 * 电邮地址：guo7892000@126.com		
 * 微 信 号：BreezeeHui		
 * 修改历史：		
 *      2022/11/5 22:29:28 新建 黄国辉 		
 *******************************************************************/
namespace Breezee.Core.WinFormUI
{
    /// <summary>
    /// 数据库列与控件关系类
    /// 作用：主要用到UI设置
    /// </summary>
    public class DBColumnControlRelation
    {
        #region 针对所有控件
        private DBColumnControlRelation()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strReadColumnName">读取列名</param>
        /// <param name="ctr">控件</param>
        /// <param name="enReadSaveEnum">读取保存枚举</param>
        /// <param name="strEnptyJudgeTipName">非空提示名称</param>
        /// <param name="strSaveColumnName">保存列名</param>
        public DBColumnControlRelation(string strReadColumnName, Control ctr, DBColumnControlReadSaveEnum enReadSaveEnum, string strEnptyJudgeTipName, string strSaveColumnName, int DecimalDigits,bool isMust)
        {
            _ReadColumnName = strReadColumnName;
            _ControlName = ctr;
            _ReadSaveEnum = enReadSaveEnum;
            _SaveColumnName = strSaveColumnName;
            _NotNullJudgeTipName = strEnptyJudgeTipName;
            _DecimalDigits = DecimalDigits;//默认为-1，即不管
            _isMust = isMust;
        }

        public DBColumnControlRelation(string strReadColumnName, Control ctr, DBColumnControlReadSaveEnum enReadSaveEnum, string strEnptyJudgeTipName, string strSaveColumnName,bool isMust)
            : this(strReadColumnName, ctr, enReadSaveEnum, strEnptyJudgeTipName, strSaveColumnName, -1,isMust)
        {
        }

        /// <summary>
        /// 构造函数（读取列名一样，可空）
        /// </summary>
        /// <param name="strReadSaveColumnName">读取保存列名</param>
        /// <param name="ctr">控件</param>
        public DBColumnControlRelation(string strReadColumnName, Control ctr, bool isMust=false)
            : this(strReadColumnName, ctr, DBColumnControlReadSaveEnum.ReadAndSave, string.Empty, strReadColumnName, -1, isMust)
        {

        }

        public DBColumnControlRelation(string strReadColumnName, Control ctr, int DecimalDigits, bool isMust = false)
            : this(strReadColumnName, ctr, DBColumnControlReadSaveEnum.ReadAndSave, string.Empty, strReadColumnName, DecimalDigits, isMust)
        {
        }

        /// <summary>
        /// 构造函数（读取列名一样，非空）
        /// </summary>
        /// <param name="strReadSaveColumnName">读取保存列名</param>
        /// <param name="ctr"></param>
        /// <param name="strEnptyJudgeTipName"></param>
        public DBColumnControlRelation(string strReadColumnName, Control ctr, string strEnptyJudgeTipName, bool isMust = true)
            : this(strReadColumnName, ctr, DBColumnControlReadSaveEnum.ReadAndSave, strEnptyJudgeTipName, strReadColumnName, -1, isMust)
        {
        }

        /// <summary>
        /// 构造函数（读取列名一样，可空，可指定读取保存类型）
        /// </summary>
        /// <param name="strReadColumnName"></param>
        /// <param name="ctr"></param>
        /// <param name="enReadSaveEnum"></param>
        public DBColumnControlRelation(string strReadColumnName, Control ctr, DBColumnControlReadSaveEnum enReadSaveEnum, bool isMust = false)
            : this(strReadColumnName, ctr, enReadSaveEnum, string.Empty, strReadColumnName, -1,isMust)
        {
        }

        public DBColumnControlRelation(string strReadColumnName, Control ctr, DBColumnControlReadSaveEnum enReadSaveEnum, int DecimalDigits, bool isMust = false)
            : this(strReadColumnName, ctr, enReadSaveEnum, string.Empty, strReadColumnName, DecimalDigits,isMust)
        {
        }

        /// <summary>
        /// 构造函数（读取列名一样，非空，可指定读取保存类型）
        /// </summary>
        /// <param name="strReadColumnName">读取列名</param>
        /// <param name="ctr">控件</param>
        /// <param name="enReadSaveEnum">读取保存枚举</param>
        /// <param name="strEnptyJudgeTipName">非空提示名称</param>
        public DBColumnControlRelation(string strReadColumnName, Control ctr, DBColumnControlReadSaveEnum enReadSaveEnum, string strEnptyJudgeTipName, bool isMust = false)
            : this(strReadColumnName, ctr, enReadSaveEnum, strEnptyJudgeTipName, strReadColumnName, -1, isMust)
        {
        }

        public DBColumnControlRelation(string strReadColumnName, Control ctr, DBColumnControlReadSaveEnum enReadSaveEnum, string strEnptyJudgeTipName, int DecimalDigits, bool isMust = false)
            : this(strReadColumnName, ctr, enReadSaveEnum, strEnptyJudgeTipName, strReadColumnName, DecimalDigits, isMust)
        {
        }
        #endregion

        #region 针对下拉框
        /// <summary>
        /// 构造下拉框的列控件关系（读存列名不同）
        /// </summary>
        /// <param name="strReadColumnName"></param>
        /// <param name="ctr"></param>
        /// <param name="seEnum"></param>
        /// <param name="strComboBoxSourceTableColumnName"></param>
        /// <param name="strSaveColumnName"></param>
        public DBColumnControlRelation(string strReadColumnName, ComboBox ctr, DBColumnComboBoxSaveEnum seEnum, DBColumnControlReadSaveEnum enReadSaveEnum, string strComboBoxSourceTableColumnName, string strSaveColumnName)
        {
            _ReadColumnName = strReadColumnName;
            _ControlName = ctr;
            _ReadSaveEnum = enReadSaveEnum;
            _NotNullJudgeTipName = string.Empty;
            _ComboBoxSaveEnum = seEnum;
            _ComboBoxSaveSourceTableColumnName = strComboBoxSourceTableColumnName;
            _SaveColumnName = strSaveColumnName;
        } 

        /// <summary>
        /// 构造下拉框的列控件关系（读存列名不同）
        /// </summary>
        /// <param name="strReadColumnName"></param>
        /// <param name="ctr"></param>
        /// <param name="seEnum"></param>
        /// <param name="strComboBoxSourceTableColumnName"></param>
        /// <param name="strSaveColumnName"></param>
        public DBColumnControlRelation(string strReadColumnName, ComboBox ctr, DBColumnComboBoxSaveEnum seEnum, string strComboBoxSourceTableColumnName, string strSaveColumnName)
            : this(strReadColumnName, ctr, seEnum, DBColumnControlReadSaveEnum.ReadAndSave, strComboBoxSourceTableColumnName, string.Empty)
        {
        } 

        /// <summary>
        /// 构造下拉框的列控件关系（读存列名相同）
        /// </summary>
        /// <param name="strReadColumnName"></param>
        /// <param name="ctr"></param>
        /// <param name="seEnum"></param>
        /// <param name="strComboBoxSourceTableColumnName"></param>
        public DBColumnControlRelation(string strReadColumnName, ComboBox ctr, DBColumnComboBoxSaveEnum seEnum, string strComboBoxSourceTableColumnName)
            : this(strReadColumnName, ctr, seEnum, strComboBoxSourceTableColumnName, string.Empty)
        {
        }

        /// <summary>
        /// 构造下拉框的列控件关系（读存列名相同）
        /// </summary>
        /// <param name="strReadColumnName"></param>
        /// <param name="ctr"></param>
        /// <param name="seEnum"></param>
        /// <param name="strComboBoxSourceTableColumnName"></param>
        public DBColumnControlRelation(string strReadColumnName, ComboBox ctr, DBColumnComboBoxSaveEnum seEnum)
            : this(strReadColumnName, ctr, seEnum, string.Empty, string.Empty)
        {
        }
        #endregion

        #region 构建方法
        public DBColumnControlRelation New()
        {
            return new DBColumnControlRelation();
        }
        public DBColumnControlRelation ReadCol(string strReadColumnName)
        {
            _ReadColumnName = strReadColumnName;
            return this;
        }

        public DBColumnControlRelation SaveCol(string sSaveColumnName)
        {
            _SaveColumnName = sSaveColumnName;
            return this;
        }

        public DBColumnControlRelation NullTip(string sTipMsg)
        {
            _NotNullJudgeTipName = sTipMsg;
            return this;
        }

        public DBColumnControlRelation Ctl(Control controlName)
        {
            _ControlName = controlName;
            return this;
        }

        public DBColumnControlRelation ReadSave(DBColumnControlReadSaveEnum readSave)
        {
            _ReadSaveEnum = readSave;
            return this;
        }

        public DBColumnControlRelation CbbSave(DBColumnComboBoxSaveEnum cbbSaveEnum)
        {
            _ComboBoxSaveEnum = cbbSaveEnum;
            return this;
        }
        public DBColumnControlRelation CbbSourceCol(string sourceTabele)
        {
            _ComboBoxSaveSourceTableColumnName = sourceTabele;
            return this;
        }

        public DBColumnControlRelation Dec(int iDec)
        {
            _DecimalDigits = iDec;
            return this;
        }

        public DBColumnControlRelation Must(bool isMust = true)
        {
            _isMust = isMust;
            return this;
        } 
        #endregion


        #region 属性
        /// <summary>
        /// 读取列名
        /// </summary>
        private string _ReadColumnName;

        public string ReadColumnName
        {
            get { return _ReadColumnName; }
            set { _ReadColumnName = value; }
        }
        /// <summary>
        /// 控件名
        /// </summary>
        private Control _ControlName;

        public Control ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }
        /// <summary>
        /// 关系读取保存枚举
        /// </summary>
        private DBColumnControlReadSaveEnum _ReadSaveEnum = DBColumnControlReadSaveEnum.ReadAndSave;

        public DBColumnControlReadSaveEnum ReadSaveEnum
        {
            get { return _ReadSaveEnum; }
            set { _ReadSaveEnum = value; }
        }
        /// <summary>
        /// 保存列名
        /// </summary>
        private string _SaveColumnName;

        public string SaveColumnName
        {
            get { return _SaveColumnName; }
            set { _SaveColumnName = value; }
        }

        /// <summary>
        /// 非空提示的列名
        /// </summary>
        private string _NotNullJudgeTipName;

        public string NotNullJudgeTipName
        {
            get { return _NotNullJudgeTipName; }
            set { _NotNullJudgeTipName = value; }
        }

        /// <summary>
        /// 下拉框保存枚举
        /// </summary>
        private DBColumnComboBoxSaveEnum _ComboBoxSaveEnum;

        public DBColumnComboBoxSaveEnum ComboBoxSaveEnum
        {
            get { return _ComboBoxSaveEnum; }
            set { _ComboBoxSaveEnum = value; }
        }

        /// <summary>
        /// 下拉框保存源表列名：针对DBColumnComboBoxSaveEnum.BindingSourceTableColumnName类型，本字段必须非空
        /// </summary>
        private string _ComboBoxSaveSourceTableColumnName;

        public string ComboBoxSaveSourceTableColumnName
        {
            get { return _ComboBoxSaveSourceTableColumnName; }
            set { _ComboBoxSaveSourceTableColumnName = value; }
        }

        /// <summary>
        /// 小数位数：针对数字型的字段
        /// </summary>
        private int _DecimalDigits;

        public int DecimalDigits
        {
            get { return _DecimalDigits; }
            set { _DecimalDigits = value; }
        }

        public bool IsMust { get => _isMust; set => _isMust = value; }

        private bool _isMust = false;

        #endregion

        
    }

    /// <summary>
    /// 数据表列与下拉框保存枚举
    /// </summary>
    public enum DBColumnComboBoxSaveEnum
    {
        /// <summary>
        /// 选择的值
        /// </summary>
        SelectValue,
        /// <summary>
        /// 选择或输入的文本
        /// </summary>
        Text,
        /// <summary>
        /// 后台绑定表列名：即要取出绑定下拉框的数据源及列名参数来获取值
        /// </summary>
        BindingSourceTableColumnName
    }

    /// <summary>
    /// 表列与控件关系的读取保存枚举
    /// </summary>
    public enum DBColumnControlReadSaveEnum
    {
        /// <summary>
        /// 只读取
        /// </summary>
        ReadOnly,
        /// <summary>
        /// 只保存
        /// </summary>
        SaveOnly,
        /// <summary>
        /// 读取并保存
        /// </summary>
        ReadAndSave
    }
}
