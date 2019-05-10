using UnityEngine;
using System.Collections;

public enum BasePropertyType
{
    HP,
    IQ,
    EQ,
    MONEY,
    //样貌
    APPEAR,
    //每个月的生活费水平
    EXPENSES
}


public class ChangePropertyCommand : BaseCommand {
    //---------改变人物属性的命令-----------

    BasePropertyType addProperty;
    private int value;

    public ChangePropertyCommand(BasePropertyType addProperty, int value, bool needAC = false)
    {
        this.addProperty = addProperty;
        this.value = value;
        this.needAsync = needAC;
    }

    public override void Excute()
    {
        switch(addProperty)
        {
            case BasePropertyType.HP:
                PlayerPropertyManager.AddPlayerHp(value);
                break;
            case BasePropertyType.IQ:
                PlayerPropertyManager.AddPlayerIQ(value);
                break;
            case BasePropertyType.EQ:
                PlayerPropertyManager.AddPlayerEQ(value);
                break;
            case BasePropertyType.MONEY:
                PlayerPropertyManager.AddPlayerMoney(value);
                break;
            case BasePropertyType.APPEAR:
                PlayerPropertyManager.AddPlayerAppear(value);
                break;
            case BasePropertyType.EXPENSES:
                PlayerPropertyManager.AddPlayerExpenses(value);
                break;
        }
    }
}
