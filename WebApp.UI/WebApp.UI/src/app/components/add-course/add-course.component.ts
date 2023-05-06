import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses.service';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css']
})
export class AddCourseComponent implements OnInit {

  createCourseRequest: Course = {
    id: '',
    title: '',
    description: '',
    startdate: new Date()
  }

  constructor(private courseService: CoursesService, private router: Router) { }

  ngOnInit(): void {
  }

  createCourse() {
     this.courseService.createCourse(this.createCourseRequest)
     .subscribe({
      next: (course) => {
        this.router.navigate(['courses'])
      },
      error: (response) => {
        console.log(response);
      }
      
     })

  }

}
