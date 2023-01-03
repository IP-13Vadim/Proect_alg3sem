package db;

import java.util.ArrayList;


public class BinarySearch {

    public static db_input binarySearch(ArrayList<db_input> arr_base, int x)
    {
        int counter = 0;
        ArrayList<db_input> arr = SparseArray.make_sparse_dense(arr_base);
        int l = 0, r = arr.size() - 1;
        while (l <= r) {
            int index = l + (r - l) / 2;

            // Check if x is present at mid
            if (arr.get(index).index == x) {
                System.out.println("Found with " + counter + " steps");
                return arr.get(index);
            }

            // If x greater, ignore left half
            if (arr.get(index).index < x) {
                l = index + 1;
            }

                // If x is smaller, ignore right half
            else {
                r = index - 1;
            }
            counter++;
        }

        // if we reach here, then element was
        // not present
        return null;
    }
}
