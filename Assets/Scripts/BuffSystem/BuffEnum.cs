public enum BuffAddType
{
    // 什么都不做
    Keep,
    // 替换已有的同名buff
    Replace,
    // 对原先的值修改
    Modify,
    
}

public enum BuffRemoveType
{
    // 直接移除
    Remove,
    // 层数减少
    ReduceStack,
    
}