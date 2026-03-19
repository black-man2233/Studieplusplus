import { apiClient } from '@/services/apiClient';

export interface WeeklyScheduleEntry {
  id: string;
  studentId: string;
  teacherId: string;
  dayOfTheWeek: number;
  startTime: string;
  endTime: string;
}

type WeeklyScheduleApiResponse = WeeklyScheduleEntry[] | { value?: WeeklyScheduleEntry[] };

export async function getWeeklySchedule(): Promise<WeeklyScheduleEntry[]> {
  const response = await apiClient.get<WeeklyScheduleApiResponse>('/api/WeeklySchedule');
  if (Array.isArray(response)) {
    return response;
  }
  return Array.isArray(response?.value) ? response.value : [];
}
