import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { Course } from '../models/course.model';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {

  baseApiUrl: string = environment.baseApiUrl

  constructor(private http: HttpClient) { }

  getAllCourses() : Observable<Course[]>{
    return this.http.get<Course[]>(this.baseApiUrl + 'api/courses/')

  }

  createCourse(createCourseRequest: Course) : Observable<Course>{
    createCourseRequest.id = '00000000-0000-0000-0000-000000000000'
    return this.http.post<Course>(this.baseApiUrl + 'api/courses/', createCourseRequest)
  }

  getCourse(id: string) : Observable<Course>{
    return this.http.get<Course>(this.baseApiUrl + 'api/courses/'+ id)

  }

  updateCourse(id: string, updateCourseRequest: Course) : Observable<Course>{
    return this.http.put<Course>(this.baseApiUrl + 'api/courses/'+ id, updateCourseRequest)
  }

  deleteCourse(id: string) : Observable<Course> {
    return this.http.delete<Course>(this.baseApiUrl + 'api/courses/'+ id)
  }

}
