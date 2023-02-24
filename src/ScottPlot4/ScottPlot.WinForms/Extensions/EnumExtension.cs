using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ScottPlot.WinForms
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 从枚举类型生成数据源并绑定到 ListControl
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="listControl">ListControl 比如 ComboBox</param>
        /// <param name="onlyValues">仅使用此值列表中的值， null 时忽略此参数</param>
        public static void MakeDataSourceAndBind<T>(ListControl listControl, IEnumerable<T> onlyValues = null)
            where T: Enum
        {
            listControl.DataSource = ToDataSource<T>(onlyValues);
            listControl.ValueMember = nameof(Tuple<T, string>.Item1);
            listControl.DisplayMember = nameof(Tuple<T, string>.Item2);
        }

        /// <summary>
        /// 获取指定的枚举类型的可绑定的数据源对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onlyValues">仅使用此值列表中的值， null 时忽略此参数</param>
        /// <returns><![CDATA[List<Tuple<枚举值, 描述字符串或枚举字符串>>]]></returns>
        public static List<Tuple<T, string>> ToDataSource<T>(IEnumerable<T> onlyValues = null) where T : Enum
        {
            var ret = new List<Tuple<T, string>>();
            var values = Enum.GetValues(typeof(T));
            for (int i = 0; i < values.Length; i++)
            {
                T v = (T)values.GetValue(i);
                if (onlyValues != null && !onlyValues.Contains(v)) continue;

                // 若被 BrowsableAttribute 标记, 判断是否显示
                var memberInfo = typeof(T).GetMember(v.ToString()).First();
                var browsableAttr = memberInfo.GetCustomAttribute<BrowsableAttribute>();
                if (browsableAttr?.Browsable == false) continue;

                ret.Add(new Tuple<T, string>(v, v.GetDescriptionString()));
            }
            return ret;
        }

        /// <summary>
        /// 获取枚举值标记为DescriptionAttribute特性的内容字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionString(this Enum value)
        {
            string ret = value.ToString();
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
                return ret;
            var field = type.GetField(name);
            if (field == null)
                return ret;
            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attr != null)
                ret = attr.Description;
            return ret;
        }

        /// <summary>
        /// 把枚举列表<see cref="List{Enum}"/>绑定到<see cref="ListControl.DataSource"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listControl"></param>
        /// <param name="enumList"></param>
        public static void BindEnumListToDataSource<T>(ListControl listControl, List<T> enumList) where T : Enum
        {
            List<Tuple<T, string>> ret = new List<Tuple<T, string>>();
            for (int i = 0; i < enumList.Count; i++)
            {
                ret.Add(new Tuple<T, string>(enumList[i], enumList[i].GetDescriptionString()));
            }
            listControl.DataSource = null;
            listControl.DataSource = ret;
            listControl.ValueMember = nameof(Tuple<T, string>.Item1);
            listControl.DisplayMember = nameof(Tuple<T, string>.Item2);
        }

        /// <summary>
        /// 把枚举列表<see cref="List{Enum}"/>绑定到<see cref="DataGridViewComboBoxColumn.DataSource"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="enumList"></param>
        public static void BindEnumListToDataSource<T>(DataGridViewComboBoxColumn column, List<T> enumList) where T : Enum
        {
            List<Tuple<T, string>> ret = new List<Tuple<T, string>>();
            for (int i = 0; i < enumList.Count; i++)
            {
                ret.Add(new Tuple<T, string>(enumList[i], enumList[i].GetDescriptionString()));
            }
            column.DataSource = null;
            column.DataSource = ret;
            column.ValueMember = nameof(Tuple<T, string>.Item1);
            column.DisplayMember = nameof(Tuple<T, string>.Item2);
        }
    }
}
