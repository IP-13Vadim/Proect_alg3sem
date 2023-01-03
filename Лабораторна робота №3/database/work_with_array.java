package db;

import java.util.ArrayList;

public class arr_work {

    public static void print_arr_list(ArrayList<db_input> arr)
    {
        for (db.db_input db_input : arr)
        {
            if(db_input!=null) System.out.println(db_input);
        }
    }

    public static ArrayList<Integer> get_indexes(ArrayList<db_input>arr)
    {
        ArrayList<Integer>indexes = new ArrayList<>();
        for (db.db_input db_input : arr) if (db_input != null) indexes.add(db_input.getIndex());
        return indexes;
    }

    public static ArrayList<Integer> get_values(ArrayList<db_input>arr)
    {
        ArrayList<Integer> indexes = new ArrayList<>();
        for (db.db_input db_input : arr) if (db_input != null) indexes.add(db_input.getValue());
        return indexes;
    }

    public static int find_max_arr_list(ArrayList<Integer> arr)
    {
        int maximum = arr.get(0);
        for (int i = 1; i < arr.size(); i++) {
            if (maximum < arr.get(i))
                maximum = arr.get(i);
        }
        return maximum;
    }

    public static String make_array_text (ArrayList<db_input>arr)
    {
        StringBuilder answer = new StringBuilder();
        for(db_input i:arr) {
            if(i!=null) {
                answer.append(i);
                answer.append(System.lineSeparator());
            }
        }
        return answer.toString();
    }


    public static ArrayList<db_input> divide_array_into_three(ArrayList<db_input>arr,int part)
    {
        arr = SparseArray.make_sparse_dense(arr);
        ArrayList<db_input> answer = new ArrayList<>();
        if(part == 1)
        {
            for(int i =0;i<arr.size()/3;i++)
            {
                answer.add(arr.get(i));
            }
        }
        else if(part==2) {
            for (int i = arr.size() / 3; i < 2 * arr.size() / 3; i++) {
                answer.add(arr.get(i));
            }

        }
        else{
            for (int i = 2* arr.size() / 3; i < arr.size(); i++) {
                answer.add(arr.get(i));
            }
        }
        return answer;
    }

}
