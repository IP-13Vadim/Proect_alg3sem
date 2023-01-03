package db;

import java.io.Serializable;

public class db_input implements Serializable {

    int index;
    int value;

    public db_input(int index, int value)
    {
        this.index = index;
        this.value = value;
    }

    public String toString() {
        return index + "  " + value;
    }

    public int getIndex()
    {
        return index;
    }

    public int getValue()
    {
        return value;
    }

}
