import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Course } from 'src/app/models/course.model';
import { CoursesService } from 'src/app/services/courses.service';

@Component({
  selector: 'app-edit-course',
  templateUrl: './edit-course.component.html',
  styleUrls: ['./edit-course.component.css']
})
export class EditCourseComponent implements OnInit {

  courseDetails: Course = {
    id: '',
    title: '',
    description: '',
    startdate: new Date()
  }

  constructor(private route: ActivatedRoute, private coursesService: CoursesService, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if(id){
          this.coursesService.getCourse(id)
          .subscribe({
            next: (response) => {
              this.courseDetails = response;
            }
          })
        }
      }
    })
  }

  updateCourse() {
    this.coursesService.updateCourse(this.courseDetails.id, this.courseDetails)
    .subscribe({
      next: (course) => {
        this.router.navigate(['courses'])
      },
      error: (response) => {
        console.log(response);
      }
      
     })
  }

  deleteCourse(id: string) {
    this.coursesService.deleteCourse(id)
     .subscribe({
      next: (response) => {
        this.router.navigate(['courses'])
      }
     })
  }

}
