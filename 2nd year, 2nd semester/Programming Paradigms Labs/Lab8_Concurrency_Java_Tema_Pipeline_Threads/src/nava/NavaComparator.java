package nava;

import java.util.Comparator;

public class NavaComparator implements Comparator<Nava>
{
    @Override
    public int compare(Nava x, Nava y)
    {
        if (x.getPriority() < y.getPriority())
        {
            return -1;
        }
        if (x.getPriority() > y.getPriority())
        {
            return 1;
        }
        return 0;
    }
}
