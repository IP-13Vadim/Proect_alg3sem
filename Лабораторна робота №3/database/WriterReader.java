package db;

import java.io.*;
import java.util.ArrayList;
import java.util.Random;

public class WriterReader {
    static final String pathname = "lab3_db.obj";
    static int amount_of_records;

    public static void setAmount_of_records(int am) throws IOException {
        amount_of_records = am;
        save_size();
    }

    public static int get_size_of_file() throws IOException {
        BufferedReader reader = new BufferedReader(new FileReader("sz_o_file.txt"));
        String line = reader.readLine();
        int size = Integer.parseInt(line);
        reader.close();
        return size;
    }

    public static void save_size () throws IOException {
        File sz = new File("sz_o_file.txt");
        FileWriter fileWriter = new FileWriter(sz);
        fileWriter.write(Integer.toString(amount_of_records));
        fileWriter.close();

    }

    public static void main(int amount_input) {
                try {
                    FileOutputStream f = new FileOutputStream(pathname);
                    ObjectOutputStream o = new ObjectOutputStream(f);
                    ArrayList<Integer> get_indexes = new ArrayList<>();
                    int counter =0;
                    for (int i = 0; i < amount_input*2; i++) {
                        Random value = new Random();
                        int rand_index = value.nextInt(amount_input + i);
                        if(!get_indexes.contains(rand_index)) {
                            db_input input = new db_input(rand_index, value.nextInt(10000));
                            get_indexes.add(rand_index);
                            // Write objects to file
                            o.writeObject(input);
                            counter++;
                        }
                    }
                    setAmount_of_records(counter);
                    save_size();
                    o.close();
                    f.close();
                } catch (FileNotFoundException e) {
                    System.out.println("File not found");
                } catch (IOException e) {
                    System.out.println("Error initializing stream");
                }
            }

    public static ArrayList<db_input> read_file() throws IOException, ClassNotFoundException {
        setAmount_of_records(get_size_of_file());
        FileInputStream fi = new FileInputStream(pathname);
        ObjectInputStream oi = new ObjectInputStream(fi);
        ArrayList<db_input> get_input = new ArrayList<>();
        for(int i =0; i<amount_of_records;i++) get_input.add(i, (db_input) oi.readObject());
        oi.close();
        fi.close();
        return get_input;
    }

    public static void write_to_db(ArrayList<db_input> new_input) throws IOException {
        new FileWriter(pathname, false).close();
        try {
            FileOutputStream f = new FileOutputStream(pathname);
            ObjectOutputStream o = new ObjectOutputStream(f);
            for (db_input input : new_input) o.writeObject(input);
            o.close();
            f.close();
        } catch (FileNotFoundException e) {
            System.out.println("File not found");
        } catch (IOException e) {
            System.out.println("Error initializing stream");
        }
    }
}