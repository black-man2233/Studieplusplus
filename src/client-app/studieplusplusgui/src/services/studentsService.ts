import { apiClient } from '@/services/apiClient';

export interface Student {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}

type StudentsApiResponse = Student[] | { value?: Student[] };

export async function getStudents(): Promise<Student[]> {
  const response = await apiClient.get<StudentsApiResponse>('/api/Students');
  if (Array.isArray(response)) {
    return response;
  }
  return Array.isArray(response?.value) ? response.value : [];
}

export function findStudentByLoginIdentifier(students: Student[], identifier: string): Student | undefined {
  const normalized = identifier.trim().toLowerCase();

  return students.find((student) => {
    const email = student.email.toLowerCase();
    return email === normalized || email.startsWith(`${normalized}@`) || email.includes(normalized);
  });
}
