import db.*;

import javax.swing.*;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;

import static interface_for_db.db_interface.create_frame;

public class Main {
    public static void main(String[] args) throws IOException, ClassNotFoundException {
        JFrame frame= new JFrame("DB");
        int amount_of_records = 100;
        File lab_file = new File("lab3_db.obj");
        if(lab_file.length()==0)
            WriterReader.main(amount_of_records);

        ArrayList<db_input> sparse_get_from_db = new ArrayList<>();

        new SparseArray(WriterReader.read_file(),sparse_get_from_db);

        SparseArray.make_dense_sparse();
        System.out.println("DB:");
        arr_work.print_arr_list(sparse_get_from_db);

        System.out.println("Overflow:");
        arr_work.print_arr_list(SparseArray.getAnswer());

        ArrayList<Integer> indexes = arr_work.get_indexes(sparse_get_from_db);
        ArrayList<Integer> values = arr_work.get_values(sparse_get_from_db);
        create_frame(frame,indexes,values);



    }


}