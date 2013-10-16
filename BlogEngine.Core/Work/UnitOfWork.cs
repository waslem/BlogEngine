﻿using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Repositorys;
using System;

namespace BlogEngine.Core.Work
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly BlogContext _context = new BlogContext();

        private BlogRepository _blogRepository;
        private CategoryRepository _categoryRepository;
        private UserRepository _userRepository;
        private CommentRepository _commentRepository;
        private VoteRepository _voteRepository;

        private bool _disposed = false;

        public IBlogRepository BlogRepository
        {
            get
            {
                if (this._blogRepository == null)
                {
                    this._blogRepository = new BlogRepository(_context);
                }
                return _blogRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (this._categoryRepository == null)
                {
                    this._categoryRepository = new CategoryRepository(_context);
                }
                return _categoryRepository;
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if (this._commentRepository == null)
                {
                    this._commentRepository = new CommentRepository(_context);
                }
                return _commentRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IVoteRepository VoteRepository
        {
            get
            {
                if (this._voteRepository == null)
                {
                    this._voteRepository = new VoteRepository(_context);
                }
                return _voteRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}