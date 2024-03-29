﻿using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

namespace Business;
    public interface IStudentService
    {
        // method definitions: method definitions must be created here in order to be used in the related controller


        IQueryable<Business.Models.StudentModel> Query();
        bool Add(Models.StudentModel model);

        bool Delete(int id);


        bool Update(Models.StudentModel model);


    }

    public class StudentService : IStudentService // UserService is a IUserService (UserService implements IUserService)
    {
        #region Db Constructor Injection
        private readonly Db _db;


        public StudentService(Db db)
        {
            _db = db;
        }
        #endregion

        // method implementations of the method definitions in the interface
        public IQueryable<Models.StudentModel> Query()
        {
            // Order the records as specified: descending by CumulativeGpa, then ascending by Name, and finally ascending by Surname
            return _db.Students.Include(e => e.Grade)
                .OrderByDescending(e => e.CumulativeGpa)
                .ThenBy(e => e.Name)
                .ThenBy(e => e.Surname)
                .Select(e => new Models.StudentModel()
                {
                    // model - entity property assignments
                    Id = e.Id,
                    Name = e.Name,
                    Surname = e.Surname,
                    UniversityExamRank = e.UniversityExamRank,
                    CumulativeGpa = e.CumulativeGpa,
                    GradeId = e.GradeId,
                    // modified model - entity property assignments for displaying in views
                    FullNameOutput = e.Name + " " + e.Surname,
                    GradeOutput = e.Grade.year
                });
        }


        public bool Add(Models.StudentModel model)
        {
            if (_db.Students.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return false;
            DataAccess.Entities.Student entity = new DataAccess.Entities.Student()
            {
                Id = model.Id,
                Name = model.Name.Trim(),
                Surname = model.Surname,
                UniversityExamRank = model.UniversityExamRank,
                CumulativeGpa = model.CumulativeGpa,
                GradeId = model.GradeId
            };
            _db.Students.Add(entity);
            _db.SaveChanges();
            return true;
        }


        public bool Delete(int id)
        {
            DataAccess.Entities.Student entity = _db.Students.SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return false;
            _db.Students.Remove(entity);
            _db.SaveChanges();
            return true;
        }


        public bool Update(Models.StudentModel model)
        {
            if (_db.Students.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Id != model.Id))
                return false;
            DataAccess.Entities.Student existingEntity = _db.Students.SingleOrDefault(s => s.Id == model.Id);
            if (existingEntity is null)
                return false;
            existingEntity.GradeId = model.GradeId;
            existingEntity.Name = model.Name.Trim();
            existingEntity.Surname = model.Surname;
            existingEntity.UniversityExamRank = model.UniversityExamRank;
            existingEntity.CumulativeGpa = model.CumulativeGpa;

            _db.Students.Update(existingEntity);
            _db.SaveChanges();
            return true;
        }
}
