package interface_for_db;
import db.*;
import javax.swing.*;
import java.awt.*;
import java.io.IOException;
import java.util.ArrayList;

import static javax.swing.WindowConstants.EXIT_ON_CLOSE;

public class db_interface {

    public static void create_frame(JFrame frame, ArrayList<Integer>indexes, ArrayList<Integer>values)
    {
        JPanel panel = new JPanel();
        panel.setAutoscrolls(true);
        panel.setLayout(new GridBagLayout());
        GridBagConstraints c = new GridBagConstraints();
        c.gridx = 2;
        c.gridy = 0;
        // create a label to display text
        JLabel l = new JLabel();

        // add text to label
        l.setText("Database:");

        panel.add(l,c);

        show_db(panel,c, SparseArray.getSparseArray());

        find_by_key(panel,c, SparseArray.getSparseArray());

        c.gridx = 2;
        c.gridy = values.size()+1;
        JLabel text_change = new JLabel("CHANGE DATABASE: ");
        panel.add(text_change,c);

        c.gridx = 1;
        c.gridy = values.size()+3;
        JTextField input_index = new JTextField("index");
        panel.add(input_index,c);
        c.gridx = 3;
        c.gridy = values.size()+3;
        JTextField input_value = new JTextField("value");
        panel.add(input_value,c);

        buttons_add_del(frame, c,indexes,values,panel,input_index,input_value, SparseArray.getOverflowBucket());

        frame.add(panel);
        frame.setDefaultCloseOperation(EXIT_ON_CLOSE );
        frame.pack();
        frame.setLocationRelativeTo( null );
        frame.setVisible(true);
        Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setSize(screenSize.width, screenSize.height);
    }

    static void show_db(JPanel panel, GridBagConstraints c, ArrayList<db_input> db)
    {

        c.gridx=1;
        c.gridy=2;

        ArrayList<db_input> part_1 = arr_work.divide_array_into_three(db,1);
        String part_1_str = arr_work.make_array_text(part_1);
        JTextArea part_1_text = new JTextArea(part_1_str);
        part_1_text.setEditable(false);
        panel.add(part_1_text,c);

        c.gridx=2;
        c.gridy=2;

        ArrayList<db_input> part_2 = arr_work.divide_array_into_three(db,2);
        String part_2_str = arr_work.make_array_text(part_2);
        JTextArea part_2_text = new JTextArea(part_2_str);
        part_2_text.setEditable(false);
        panel.add(part_2_text,c);

        c.gridx=3;
        c.gridy=2;

        ArrayList<db_input> part_3 = arr_work.divide_array_into_three(db,3);
        String part_3_str = arr_work.make_array_text(part_3);
        JTextArea part_3_text = new JTextArea(part_3_str);
        part_3_text.setEditable(false);
        panel.add(part_3_text,c);




    }

    public static void buttons_add_del(JFrame frame, GridBagConstraints c, ArrayList<Integer>indexes, ArrayList<Integer>values,JPanel panel,JTextField input_index,JTextField input_value, ArrayList<db_input>overflow_bucket)   {
        c.gridx = 1;
        c.gridy = values.size()+4;
        JButton b_add=new JButton("Add");
        b_add.setBounds(50,100,95,30);
        b_add.addActionListener(e -> {
            SwingUtilities.updateComponentTreeUI(panel);
            String str_us_index = input_index.getText();
            String str_us_value = input_value.getText();
            if (!str_us_value.equals("value") && !str_us_index.equals("index")) {

                int user_index = Integer.parseInt(str_us_index);
                int user_value = Integer.parseInt(str_us_value);
                db_input user_input = new db_input(user_index, user_value);
                ArrayList<db_input>new_db =new ArrayList<>();
                if(!indexes.contains(user_index)) {
                    for (int i = 0; i < indexes.size(); i++) {
                        db_input input = new db_input(indexes.get(i), values.get(i));
                        new_db.add(input);
                    }
                    new_db.add(user_input);
                    try {
                        frame.setVisible(false);
                        frame.dispose();

                        WriterReader.write_to_db(new_db);
                        with_changes.add_changes(new_db);
                    } catch (IOException | ClassNotFoundException ex) {
                        throw new RuntimeException(ex);
                    }
                }


            }
        });
        panel.add(b_add,c);



        c.gridx = 2;
        c.gridy = values.size()+4;
        JButton b_edit=new JButton("Edit");
        b_edit.setBounds(50,100,95,30);
        b_edit.addActionListener(e -> {
            SwingUtilities.updateComponentTreeUI(panel);
            String str_us_index = input_index.getText();
            String str_us_value = input_value.getText();
            if (!str_us_value.equals("value") && !str_us_index.equals("index")) {

                int user_index = Integer.parseInt(str_us_index);
                int user_value = Integer.parseInt(str_us_value);

                if(indexes.contains(user_index))
                {
                    int ind = indexes.indexOf(user_index);
                    values.set(ind,user_value);


                }


                ArrayList<db_input>new_db =new ArrayList<>();
                for(int i =0; i<indexes.size();i++)
                {
                    db_input input = new db_input(indexes.get(i),values.get(i));
                    new_db.add(input);
                }

                try {
                    frame.setVisible(false);
                    frame.dispose();

                    WriterReader.write_to_db(new_db);
                    with_changes.add_changes(new_db);
                } catch (IOException | ClassNotFoundException ex) {
                    throw new RuntimeException(ex);
                }


            }
        });
        panel.add(b_edit,c);


        c.gridx = 3;
        c.gridy = values.size()+4;
        JButton b_del=new JButton("Delete");
        b_del.setBounds(50,100,95,30);
        panel.add(b_del,c);
        b_del.addActionListener(e -> {
            SwingUtilities.updateComponentTreeUI(panel);
            String str_us_index = input_index.getText();
            String str_us_value = input_value.getText();
            if (!str_us_value.equals("value") && !str_us_index.equals("index")) {

                int user_index = Integer.parseInt(str_us_index);
                int user_value = Integer.parseInt(str_us_value);
                if (values.contains(user_value) && indexes.contains(user_index)) {
                    int del_index = values.indexOf(user_value);
                    ArrayList<db_input> new_db = new ArrayList<>();
                    for (int i = 0; i < indexes.size(); i++) {
                        db_input input = new db_input(indexes.get(i), values.get(i));
                        if(i!=del_index)
                            new_db.add(input);
                        else if(overflow_bucket.size()>0){
                            new_db.add(overflow_bucket.get(0));
                            overflow_bucket.remove(0);
                        }

                    }
                    SparseArray.setAnswer(overflow_bucket);
                    try {
                        frame.setVisible(false);
                        frame.dispose();

                        WriterReader.write_to_db(new_db);
                        with_changes.add_changes(new_db);
                    } catch (IOException | ClassNotFoundException ex) {
                        throw new RuntimeException(ex);
                    }


                }
            }
        });



    }


    public static void find_by_key(JPanel panel, GridBagConstraints c,ArrayList<db_input>arr)
    {
        c.gridx = 0;
        c.gridy = arr.size()+5;
        final JLabel[] text_find = {new JLabel("FIND BY KEY: ")};
        panel.add(text_find[0],c);
        c.gridx = 1;
        c.gridy = arr.size()+5;

        JTextField input_key = new JTextField("key");
        panel.add(input_key,c);

        c.gridx = 2;
        c.gridy = arr.size()+5;
        JButton b_find=new JButton("FIND");
        b_find.setBounds(50,100,95,30);
        JTextArea text_find_result = new JTextArea();
        text_find_result.setEditable(false);

        b_find.addActionListener(e -> {
            SwingUtilities.updateComponentTreeUI(panel);
            String str_us_key = input_key.getText();
            System.out.println(input_key);

            if (!input_key.equals("key")) {
                ArrayList<db_input>arr_copy = new ArrayList<>(arr);
                SwingUtilities.updateComponentTreeUI(panel);
                int user_key = Integer.parseInt(str_us_key);
                db_input search_res = new db_input(1,1);
                ArrayList<db_input>answers = new ArrayList<>();
                while (search_res!=null) {
                    search_res = BinarySearch.binarySearch(arr_copy, user_key);
                    answers.add(search_res);
                    arr_copy.remove(search_res);
                }
                answers.remove(answers.size()-1);

                c.gridx = 2;
                c.gridy = arr.size()+6;

                if(!answers.isEmpty())
                {
                    String answer = arr_work.make_array_text(answers);
                    text_find_result.setText("Found:  \n" + answer);
                }
                else
                {
                    text_find_result.setText("NOT FOUND");
                }
                panel.add(text_find_result,c);
            }
        });
        panel.add(b_find,c);
    }
}