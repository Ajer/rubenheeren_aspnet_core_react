import React,{useState} from 'react';
import './App.css';
import Constants from './Utilities/Constants';
import PostCreateForm from './components/PostCreateForm';
import PostUpdateForm from './components/PostUpdateForm';

function App() {
  const [posts,setPosts] = useState([]);
  const [showingCreatePostForm,setShowingCreatePostForm] = useState(false);
  const [postCurrentlyBeingUpdated,setPostCurrentlyBeingUpdated] = useState(null);

  function getPosts()
  {
    const url = Constants.API_URL_GET_ALL_POSTS; /*Constants.API_URL_GET_ALL_POSTS;*/
    fetch(url,{
      method: 'GET'
    }).then(response =>response.json()).then(postsFromServer => {
      setPosts(postsFromServer);
      }).catch((error)=>{
         console.log(error);
         alert(error);
      });
  }
  
  return (
    <div className="container">
      <div className="row min-vh-100">
        <div className="col d-flex flex-column justify-content-center align-items-center">
          {(showingCreatePostForm===false && postCurrentlyBeingUpdated===null) && (       
              <div>
                 <h1>ASP.NET React Tutorial</h1>
              
                 <div className="mt-5">
                    <button onClick={getPosts} className="btn btn-dark btn-lg w-100">Get Posts from server</button>
                    <button onClick={() => {setShowingCreatePostForm(true)} } className="btn btn-secondary btn-lg w-100 mt-4">Create new Post</button>
                 </div>
              </div>
            )}

            {(posts.length>0 && showingCreatePostForm===false && postCurrentlyBeingUpdated===null) && renderPostTable()}
            {showingCreatePostForm && <PostCreateForm onPostCreated={onPostCreated} />}
            {postCurrentlyBeingUpdated!==null && <PostUpdateForm post={postCurrentlyBeingUpdated} onPostUpdated={onPostUpdated} />}
        </div>
      </div>
    </div>
  );


  function renderPostTable()
  {
    return(
     <div className="table-responsive mt-5 w-100">
        <table className="table table-responsive table-bordered border-dark w-100">
           <thead>
            <tr>
              <th scope="col">PostId(PK)</th>
              <th scope="col">Title</th>
              <th scope="col">Content</th>
              <th scope="col">Created Time</th>
              <th scope="col">CRUD Operations</th>
            </tr>
           </thead>
           <tbody>
            {posts.map((post)=>(
            <tr key={post.postId}>
               <th scope="row">{post.postId}</th>
               <td>{post.title}</td>
               <td>{post.content}</td>
               <td>{post.createdTime}</td>
               <td>
                 <button onClick={() => setPostCurrentlyBeingUpdated(post)} className="btn btn-dark btn-lg mx-3 my-3">Update</button>
                 <button onClick={() => {if(window.confirm('Are you shure yoy want to delete the post?')) deletePost(post.postId) }}   className="btn btn-secondary btn-lg">Delete</button>
               </td>
            </tr>))}
           </tbody>
        </table>
        <button onClick={()=>setPosts([])} className="btn btn-dark btn-lg w-100">reset</button>
     </div>
    );
  }

  function deletePost(id)
  {
    if (id>=1)
    {
      const url = `${Constants.API_URL_DELETE_POST}/${id}`;

      fetch(url,{
      method: 'DELETE'
      }).then(response =>response.json()).then(responseFromServer => {
         console.log(responseFromServer);
         onPostDeleted(id);     
      }).catch((error)=>{
         console.log(error);
         alert(error);
      });
    }
  }

  function onPostCreated(createdPost)
  {
    setShowingCreatePostForm(false);

    if (createdPost===null)
    {
       return;
    }
    alert('post created');
    getPosts();
  }

  function onPostUpdated(updatedPost)
  {
    setPostCurrentlyBeingUpdated(null);

    if (updatedPost===null)
    {
       return;
    }
    
    let postsCopy = [...posts];

    const index = postsCopy.findIndex((postsCopyPost,currentIndex) =>{
       if(postsCopyPost.postId===updatedPost.postId)
       {
            return true;
       }
    });

    if (index!==-1)
    {
         postsCopy[index] = updatedPost;
    }

    setPosts(postsCopy);

    alert(`Post successfully updated. Post updated with title ${updatedPost.title}`);
  }

  function onPostDeleted(deletedPostId)
  {
    let theTitle = '';

    let postsCopy = [...posts];

    const index = postsCopy.findIndex((postsCopyPost,currentIndex) =>{
       if(postsCopyPost.postId===deletedPostId)
       {
            return true;
       }
    });

    if (index!==-1)
    {
        theTitle = postsCopy[index].title;
        postsCopy.splice(index,1);
    }

    setPosts(postsCopy);

    alert(`Post successfully Deleted. Post with title ${theTitle} deleted.`);
  }
  
}

export default App;
