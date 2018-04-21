using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
public class ResultModel_Common
{
    public ResultModel_Common()
    {
        _Result = 0;
        _Desc = "";
        _Data = null;
    }

    private int _Result;
    /// <summary>
    /// 返回标识  0-失败  1-成功
    /// </summary>
    public int Result
    {
        get { return _Result; }
        set { _Result = value; }
    }

    private string _Desc;
    /// <summary>
    /// 返回值说明
    /// </summary>
    public string Desc
    {
        get { return _Desc; }
        set { _Desc = value; }
    }

    private object _Data;
    /// <summary>
    /// 返回类
    /// </summary>
    public object Data
    {
        get { return _Data; }
        set { _Data = value; }
    }
}


public class ResultModel_Common<T>
{
    public ResultModel_Common()
    {
        _Result = 0;
        _Desc = "";

    }

    private int _Result;
    /// <summary>
    /// 返回标识  0-失败  1-成功
    /// </summary>
    public int Result
    {
        get { return _Result; }
        set { _Result = value; }
    }

    private string _Desc;
    /// <summary>
    /// 返回值说明
    /// </summary>
    public string Desc
    {
        get { return _Desc; }
        set { _Desc = value; }
    }

    private T _Data;
    /// <summary>
    /// 返回类
    /// </summary>
    public T Data
    {
        get { return _Data; }
        set { _Data = value; }
    }
}