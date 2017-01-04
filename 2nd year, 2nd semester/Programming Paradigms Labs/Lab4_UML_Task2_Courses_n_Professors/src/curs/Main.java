package curs;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Main {

	public static void main(String[] args) {
		Course course = new Course(10);
		
		while(true) {
			BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
			String line = new String();
			
			System.out.println("");
			System.out.println("Course state: " + course.getState());
			System.out.println("Registration: " + course.getRegistrationStatus());
			System.out.println("Number of students: " + course.getNumStudents());
			System.out.println("Capacity: " + course.getCapacity());
			System.out.println("1. Add professor;");
			System.out.println("2. Remove professor;");
			System.out.println("3. Add student;");
			System.out.println("4. Remove student;");
			System.out.println("5. Close registration;");
			
			try {
				line = br.readLine();
			} catch (IOException e) {
				
			}
			
			switch(line) {
			case "1":
				course.addProfessor();
				break;
			case "2":
				course.removeProfessor();
				break;
			case "3":
				course.addStudent();
				break;
			case "4":
				course.removeStudent();
				break;
			case "5":
				course.closeRegistration();
				break;
			}
		}
	}

}
