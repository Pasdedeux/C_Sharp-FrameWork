using System;


/// <summary>
/// 屏蔽字检测工具
/// </summary>
public abstract class WordsFilter
{
    /// <summary>
    /// 敏感字数组
    /// </summary>
    public static string[] s_filters = null;

    /// <summary>
    /// 敏感词连续字符中至少插入 filter_deep 个词才能使该词通过检查
    /// </summary>
    public int filter_deep = 1;

    /// <summary>
    /// 是否通过敏感词检测
    /// </summary>
    /// <param name="content">待检测词组</param>
    /// <returns></returns>
	public static bool isPassDetecting( string content )
    {
        //如果敏感词配置未正确加载视为无限制
        if ( s_filters.Count < 1 || s_filters == null ) return true;

        //去除待检测词组空白符
        content = content.Trim();
        //字符长度不足
        if ( content.Length < 1 )
        {
            //TODO 长度不足提示
            //..

            return false;
        }

        foreach ( var e in list )
        {
            if ( e.Length > 0 )
            {
                bool bFiltered = true;
                while ( bFiltered )
                {
                    //上一个敏感词字符索引
                    int result_index_last = -1;
                    int idx = 0;

                    while ( idx < e.Length )
                    {
                        string one_s = e.Substring( idx , 1 );
                        //忽略大小写检测是否包含敏感词字符
                        //当前敏感词字符索引
                        int new_index = content.IndexOf( one_s , result_index_last + 1 , System.StringComparison.OrdinalIgnoreCase );
                        if ( new_index == -1 )
                        {
                            //当前敏感词没有匹配字符则进行下一个敏感词检测
                            bFiltered = false;
                            break;
                        }

                        //检测两个字符之间插入 filter_deep 个其他字符方可通过验证
                        if ( idx > 0 && new_index - result_index_last > filter_deep )
                        {
                            bFiltered = false;
                            break;
                        }
                        result_index_last = new_index;

                        idx++;
                    }

                    if ( bFiltered )
                    {
                        //TODO 含有敏感词，未通过验证提示
                        //..
                        return false;
                    }
                }
            }
        }

        //验证成功

        //TODO 释放敏感词缓存
        s_filters = null;

        return true;

    }
}
