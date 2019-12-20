﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HF.Cloud.CommonDAL
{
   public static  class HongBao
    {
        /// <summary>
        /// 发红包
        /// </summary>
        /// <param name="HbTotal">总金额</param>
        /// <param name="HbNumber">红包数(红包数要小于总金额，根据最小值来具体判断)</param>
        /// <param name="HbMin">红包最小值（填0为默认0.01）</param>
        /// <returns></returns>
        public static List<double> GetRedBagList(double HbTotal, int HbNumber, double HbMin)
        {
            List<double> listHB = new List<double>();

            //初始化要发起的红包基础数据
            double total = HbTotal;
            int num = HbNumber;
            double min = 0.01;//默认为0.01

            if (HbMin != 0)
            {
                min = HbMin;
            }

            total -= min * num;
            if (total < 0)
            {
                return listHB;
            }

            if (total > 0 && num > 0 && min > 0)
            {
                //产生正态分布的随机红包金额，并计算相关的金额和数量保证数据的准确性
                double average = total / num;
                double variance = 1;
                Random u1 = new Random();
                Random u2 = new Random();
                double[] nums = new double[num];

                for (int i = 0; i < num; i++)
                {
                    double? result = total;
                    if (i < num - 1 && total > 0)
                    {
                        do
                        {
                            result = Round((double)Normal(u1.NextDouble(), u2.NextDouble(), average, variance), 2);
                        } while (result == null || result < 0);
                        if (total > result)
                        {
                            total = (double)Round((total - (double)result), 2);
                        }
                        else
                        {
                            result = total;
                            total = 0;
                        }
                    }
                    else if (i == num - 1)
                    {
                        total = 0;
                    }
                    nums[i] = Math.Round(min + (double)result, 2); //浮点运算问题，这里需要四舍五入数据才正确

                    double hbItem = min + (double)result;
                    listHB.Add(hbItem);
                }
                return listHB;
            }
            else
            {
                return listHB;
            }
        }


        /// <summary>
        /// 产生符合正态分布的随机数
        /// </summary>
        /// <param name="u1">正态分布第一个随机数</param>
        /// <param name="u2">正态分布第二个随机数</param>
        /// <param name="averageValue">正态期望(平均值)</param>
        /// <param name="variance">正态标准差(Math.Sqrt(方差))</param>
        /// <returns></returns>
        public static double? Normal(double u1, double u2, double averageValue, double variance)
        {
            double? result = null;
            try
            {
                result = averageValue + Math.Sqrt(variance) * Math.Sqrt((-2) * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 求一组数据的方差
        /// </summary>
        /// <param name="list">要求的数组</param>
        /// <returns></returns>
        public static double Variance(double[] nums)
        {
            double average = nums.Sum() / nums.Length;
            double sum = 0;
            double variance = 0;
            foreach (double num in nums)
            {
                sum += Math.Pow((num - average), 2);
            }
            variance = sum / nums.Length;

            return variance;
        }

        /// <summary>
        /// 截取小数指定小数位，且不四舍五入
        /// </summary>
        /// <param name="originNum">要截取的小数</param>
        /// <param name="lastNum">截取小数后位数</param>
        /// <returns></returns>
        public static double? Round(double originNum, int lastNum)
        {
            double? result = null;
            int index = originNum.ToString().IndexOf('.');
            if (index != -1)
            {
                string temp = originNum.ToString();
                result = Convert.ToDouble(temp.Substring(0, index + 1) + temp.Substring(index + 1, Math.Min(temp.Length - index - 1, lastNum)));
            }
            if (result == 0)
            {
                result = null;
            }
            else if (index == -1)
            {
                result = originNum;
            }

            return result;
        }






    }
}