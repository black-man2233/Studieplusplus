import { apiClient } from '@/services/apiClient';

export interface Teacher {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  specializations?: string[];
}

type TeachersApiResponse = Teacher[] | { value?: Teacher[] };

export async function getTeachers(): Promise<Teacher[]> {
  const response = await apiClient.get<TeachersApiResponse>('/api/Teachers');
  if (Array.isArray(response)) {
    return response;
  }
  return Array.isArray(response?.value) ? response.value : [];
}
