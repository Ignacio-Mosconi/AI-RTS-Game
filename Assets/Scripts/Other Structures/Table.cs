using System;
using UnityEngine;

public class Table<R, C, V>
{
    R[] rows;
    C[] columns;
    V[,] values;

    public Table(R[] rows, C[] columns)
    {
        this.rows = rows;
        this.columns = columns;
        values = new V[rows.Length, columns.Length];
    }

    public V this[R row, C column]
    {
        get
        {
            V value = default;

            try
            {
                int i = Array.IndexOf(rows, row);
                int j = Array.IndexOf(columns, column);

                if (i == -1 || j == -1)
                    throw new IndexOutOfRangeException();

                value = values[i, j];
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.LogError(e.Message);
            }

            return value;
        }

        set
        {
            try
            {
                int i = Array.IndexOf(rows, row);
                int j = Array.IndexOf(columns, column);

                if (i == -1 || j == -1)
                    throw new IndexOutOfRangeException();

                values[i, j] = value;
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}